using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using Two.Common.Application;
using Two.InGame.Application;
using Two.InGame.Domain.UseCase.Interface;
using UnityEngine;
using VContainer;

namespace Two.InGame.Presentation.View
{
    public sealed class MatchingUserView : MonoBehaviour
    {
        [SerializeField] private RectTransform leftSide = default;
        [SerializeField] private RectTransform rightSide = default;
        [SerializeField] private TextMeshProUGUI masterName = default;
        [SerializeField] private TextMeshProUGUI clientName = default;

        private IMatchingUseCase _matchingUseCase;

        [Inject]
        private void Construct(IMatchingUseCase matchingUseCase)
        {
            _matchingUseCase = matchingUseCase;
        }

        public void Init()
        {
            masterName.text = "";
            clientName.text = "";
        }

        public async UniTask ShowPlayerNameAsync(CancellationToken token)
        {
            masterName.text = _matchingUseCase.GetUserName(PlayerType.Master);
            clientName.text = _matchingUseCase.GetUserName(PlayerType.Client);

            await (
                leftSide
                    .DOAnchorPosX(240.0f, AnimationTime.UI_MOVE)
                    .SetEase(Ease.InQuart)
                    .WithCancellation(token),
                rightSide
                    .DOAnchorPosX(-240.0f, AnimationTime.UI_MOVE)
                    .SetEase(Ease.InQuart)
                    .WithCancellation(token)
            );

            await UniTask.Delay(TimeSpan.FromSeconds(1.0f), cancellationToken: token);
        }

        public async UniTask HidePlayerNameAsync(CancellationToken token)
        {
            await (
                leftSide
                    .DOAnchorPosX(-240.0f, AnimationTime.UI_MOVE)
                    .SetEase(Ease.InQuart)
                    .WithCancellation(token),
                rightSide
                    .DOAnchorPosX(240.0f, AnimationTime.UI_MOVE)
                    .SetEase(Ease.InQuart)
                    .WithCancellation(token)
            );

            await UniTask.Delay(TimeSpan.FromSeconds(1.0f), cancellationToken: token);
        }
    }
}