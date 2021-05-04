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
        [SerializeField] private TextMeshProUGUI rateText = default;
        [SerializeField] private Button backTitleButton = default;
        [SerializeField] private Button tweetButton = default;
        [SerializeField] private Image backGround = default;

        private IMatchingUseCase _matchingUseCase;
        private IRatingUseCase _ratingUseCase;

        [Inject]
        private void Construct(IMatchingUseCase matchingUseCase, IRatingUseCase ratingUseCase)
        {
            _matchingUseCase = matchingUseCase;
            _ratingUseCase = ratingUseCase;
        }

        public override GameState GetState() => GameState.Result;

        public override UniTask InitAsync(CancellationToken token)
        {
            resultText.text = "";
            rateText.text = "";
            ActivateButton(false);
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
            rateText.text = $"{_matchingUseCase.GetPlayerRate()}";

            await backGround.rectTransform
                .DOAnchorPosY(0.0f, AnimationTime.UI_MOVE)
                .SetEase(Ease.OutBounce)
                .WithCancellation(token);

            // レート計算
            var addRateValue = _matchingUseCase.GetAddRateValue();
            var currentRate = _matchingUseCase.GetPlayerRate() + addRateValue;
            await (
                DOTween.To(
                        () => _matchingUseCase.GetPlayerRate(),
                        x => rateText.text = $"{x}",
                        currentRate,
                        AnimationTime.UI_MOVE)
                    .WithCancellation(token),
                _ratingUseCase.SendScoreAsync(currentRate, token)
            );

            ActivateButton(true);

            return GameState.None;
        }

        public override UniTask DisposeAsync(CancellationToken token)
        {
            return base.DisposeAsync(token);
        }

        private void ActivateButton(bool value)
        {
            backTitleButton.gameObject.SetActive(value);
            tweetButton.gameObject.SetActive(value);
        }
    }
}