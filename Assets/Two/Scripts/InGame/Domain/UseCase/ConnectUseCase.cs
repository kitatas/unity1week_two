using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Photon.Pun;
using Photon.Realtime;
using Pun2Task;
using Two.Common.Domain.Repository.Interface;
using Two.InGame.Application;
using Two.InGame.Data.Entity.Interface;
using Two.InGame.Domain.UseCase.Interface;

namespace Two.InGame.Domain.UseCase
{
    public sealed class ConnectUseCase : IConnectUseCase
    {
        private readonly IMatchingEntity _matchingEntity;
        private readonly INameRepository _nameRepository;

        public ConnectUseCase(IMatchingEntity matchingEntity, INameRepository nameRepository)
        {
            _matchingEntity = matchingEntity;
            _nameRepository = nameRepository;
        }

        public async UniTask JoinRoomAsync(CancellationToken token)
        {
            await Pun2TaskNetwork.ConnectUsingSettingsAsync(token);

            JoinRandomRoomAsync(token).Forget();

            await Pun2TaskCallback.OnJoinedRoomAsync();

            PhotonNetwork.LocalPlayer.NickName = _nameRepository.Load();
        }

        private static async UniTaskVoid JoinRandomRoomAsync(CancellationToken token)
        {
            PhotonNetwork.JoinRandomRoom();

            await Pun2TaskCallback.OnJoinRandomFailedAsync();

            var roomOptions = new RoomOptions
            {
                IsVisible = true,
                IsOpen = true,
                MaxPlayers = 2,
                CustomRoomProperties = new ExitGames.Client.Photon.Hashtable {{"CustomProperties", "カスタムプロパティ"}},
                CustomRoomPropertiesForLobby = new[] {"CustomProperties"}
            };

            var roomName = Guid.NewGuid().ToString("N").Substring(0, 16);
            await Pun2TaskNetwork.CreateRoomAsync(roomName, roomOptions, null, token: token);
        }

        public async UniTask<PlayerType> MatchingAsync(CancellationToken token)
        {
            await UniTask.WaitUntil(IsMaxPlayer, cancellationToken: token);

            PhotonNetwork.CurrentRoom.IsOpen = false;
            await UniTask.Delay(TimeSpan.FromSeconds(1.0f), cancellationToken: token);

            var playerName = _nameRepository.Load();
            var enemyName = PhotonNetwork.PlayerListOthers[0].NickName;
            _matchingEntity.SetMatchingUserName(playerName, enemyName);

            var playerType = PhotonNetwork.IsMasterClient ? PlayerType.Master : PlayerType.Client;
            var enemyType = PhotonNetwork.IsMasterClient ? PlayerType.Client : PlayerType.Master;
            _matchingEntity.SetMatchingUserType(playerType, enemyType);
            return playerType;
        }

        private static bool IsMaxPlayer() => IsPlayerCount(PhotonNetwork.CurrentRoom.MaxPlayers);

        private static bool IsPlayerCount(int count) => PhotonNetwork.CurrentRoom?.PlayerCount == count;

        public void Disconnect()
        {
            if (PhotonNetwork.IsConnected == false)
            {
                return;
            }

            if (PhotonNetwork.CurrentRoom == null)
            {
                return;
            }

            if (IsPlayerCount(1))
            {
                PhotonNetwork.CurrentRoom.IsOpen = true;
            }

            PhotonNetwork.Disconnect();
        }
    }
}