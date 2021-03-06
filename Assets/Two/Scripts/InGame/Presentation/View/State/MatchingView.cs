using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Two.InGame.Application;
using Two.InGame.Domain.UseCase.Interface;
using Two.InGame.Factory;
using Two.InGame.Presentation.Controller;
using UnityEngine;
using VContainer;

namespace Two.InGame.Presentation.View.State
{
    public sealed class MatchingView : BaseState
    {
        private PunObjectFactory _punObjectFactory;
        private IMatchingStateUseCase _matchingStateUseCase;
        private IGameStateUseCase _gameStateUseCase;
        private IConnectUseCase _connectUseCase;
        private MatchingUserView _matchingUserView;

        [Inject]
        private void Construct(PunObjectFactory punObjectFactory, IMatchingStateUseCase matchingStateUseCase,
            IGameStateUseCase gameStateUseCase, IConnectUseCase connectUseCase, MatchingUserView matchingUserView)
        {
            _punObjectFactory = punObjectFactory;
            _matchingStateUseCase = matchingStateUseCase;
            _gameStateUseCase = gameStateUseCase;
            _connectUseCase = connectUseCase;
            _matchingUserView = matchingUserView;
        }

        public override GameState GetState() => GameState.Matching;

        public override UniTask InitAsync(CancellationToken token)
        {
            _matchingUserView.Init();
            return base.InitAsync(token);
        }

        public override UniTask ResetAsync(GameState state, CancellationToken token)
        {
            return base.ResetAsync(state, token);
        }

        public override async UniTask<GameState> TickAsync(CancellationToken token)
        {
            try
            {
                _matchingStateUseCase.SetState(MatchingState.Connecting);
                await _connectUseCase.JoinRoomAsync(token);

                _matchingStateUseCase.SetState(MatchingState.Matching);
                var playerType = await _connectUseCase.MatchingAsync(token);

                _punObjectFactory.Generate(playerType);

                _matchingStateUseCase.SetState(MatchingState.Matched);
                await UniTask.Delay(TimeSpan.FromSeconds(2.0f), cancellationToken: token);

                InitPlayer();
                await _matchingUserView.ShowPlayerNameAsync(token);

                _matchingStateUseCase.SetState(MatchingState.None);

                CheckDisconnectAsync(token).Forget();

                return GameState.Ready;
            }
            catch (Exception e)
            {
                Debug.LogError($"connect error: {e}");
                throw;
            }
        }

        public override UniTask DisposeAsync(CancellationToken token)
        {
            return base.DisposeAsync(token);
        }

        private static void InitPlayer()
        {
            var players = FindObjectsOfType<PlayerController>();
            foreach (var player in players)
            {
                player.InitPlayerInfo();
            }
        }

        private async UniTaskVoid CheckDisconnectAsync(CancellationToken token)
        {
            await UniTask.WaitUntil(() => _connectUseCase.IsPlayerCount(1), cancellationToken: token);

            // ?????????????????????
            if (_gameStateUseCase.IsEqual(GameState.Matching) ||
                _gameStateUseCase.IsEqual(GameState.Ready) ||
                _gameStateUseCase.IsEqual(GameState.Battle))
            {
                _gameStateUseCase.SetState(GameState.None);
                _matchingStateUseCase.SetState(MatchingState.Disconnected);
            }
        }
    }
}