using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Two.InGame.Application;
using Two.InGame.Presentation.Controller;

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
            var players = FindObjectsOfType<PlayerController>();
            await UniTask.WaitUntil(() =>
            {
                return players.Any(player => player.IsDead());
            }, cancellationToken: token);

            return GameState.Result;
        }

        public override UniTask DisposeAsync(CancellationToken token)
        {
            return base.DisposeAsync(token);
        }
    }
}