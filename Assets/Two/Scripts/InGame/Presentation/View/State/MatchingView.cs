using System.Threading;
using Cysharp.Threading.Tasks;
using Two.InGame.Application;

namespace Two.InGame.Presentation.View.State
{
    public sealed class MatchingView : BaseState
    {
        public override GameState GetState() => GameState.Matching;

        public override UniTask InitAsync(CancellationToken token)
        {
            return base.InitAsync(token);
        }

        public override UniTask ResetAsync(GameState state, CancellationToken token)
        {
            return base.ResetAsync(state, token);
        }

        public override async UniTask<GameState> TickAsync(CancellationToken token)
        {
            return GameState.Ready;
        }

        public override UniTask DisposeAsync(CancellationToken token)
        {
            return base.DisposeAsync(token);
        }
    }
}