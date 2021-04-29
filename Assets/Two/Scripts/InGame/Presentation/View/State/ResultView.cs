using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using TMPro;
using Two.InGame.Application;
using Two.InGame.Domain.UseCase.Interface;
using UnityEngine;
using VContainer;

namespace Two.InGame.Presentation.View.State
{
    public sealed class ResultView : BaseState
    {
        [SerializeField] private TextMeshProUGUI resultText = default;

        private IMatchingUseCase _matchingUseCase;

        [Inject]
        private void Construct(IMatchingUseCase matchingUseCase)
        {
            _matchingUseCase = matchingUseCase;
        }

        public override GameState GetState() => GameState.Result;

        public override UniTask InitAsync(CancellationToken token)
        {
            resultText.text = "";
            return base.InitAsync(token);
        }

        public override UniTask ResetAsync(GameState state, CancellationToken token)
        {
            return base.ResetAsync(state, token);
        }

        public override async UniTask<GameState> TickAsync(CancellationToken token)
        {
            // 死亡演出終了待ち
            await UniTask.Delay(TimeSpan.FromSeconds(1.0f), cancellationToken: token);

            resultText.text = $"winner: {_matchingUseCase.GetWinnerName()}";
            return GameState.None;
        }

        public override UniTask DisposeAsync(CancellationToken token)
        {
            return base.DisposeAsync(token);
        }
    }
}