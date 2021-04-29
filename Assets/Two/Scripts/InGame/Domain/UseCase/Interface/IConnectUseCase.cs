using System.Threading;
using Cysharp.Threading.Tasks;
using Two.InGame.Application;

namespace Two.InGame.Domain.UseCase.Interface
{
    public interface IConnectUseCase
    {
        UniTask JoinRoomAsync(CancellationToken token);
        UniTask<PlayerType> MatchingAsync(CancellationToken token);
        void Disconnect();
    }
}