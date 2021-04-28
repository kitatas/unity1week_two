using System.Threading;
using Cysharp.Threading.Tasks;
using Two.InGame.Application;

namespace Two.InGame.Presentation.View.State
{
    public sealed class BattleView : BaseState
    {
        public override GameState GetState() => GameState.Battle;

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
            return GameState.Result;
        }

        public override UniTask DisposeAsync(CancellationToken token)
        {
            return base.DisposeAsync(token);
        }
    }
}