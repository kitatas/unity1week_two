using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using Two.Common.Application;
using Two.InGame.Application;
using Two.InGame.Domain.UseCase.Interface;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Two.InGame.Presentation.View.State
{
    public sealed class ResultView : BaseState
    {
        [SerializeField] private TextMeshProUGUI resultText = default;
        [SerializeField] private Image backGround = default;

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

            resultText.text = $"{_matchingUseCase.GetWinnerName()}さんの勝ち";
            backGround.rectTransform
                .DOAnchorPosY(0.0f, AnimationTime.UI_MOVE)
                .SetEase(Ease.OutBounce);

            return GameState.None;
        }

        public override UniTask DisposeAsync(CancellationToken token)
        {
            return base.DisposeAsync(token);
        }
    }
}