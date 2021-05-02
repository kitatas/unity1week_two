using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using Two.Common.Application;
using Two.Common.Presentation.Controller.Sound;
using Two.InGame.Application;
using UnityEngine;
using VContainer;

namespace Two.InGame.Presentation.View.State
{
    public sealed class ReadyView : BaseState
    {
        [SerializeField] private TextMeshProUGUI countDownText = default;

        private MatchingUserView _matchingUserView;
        private SeController _seController;

        [Inject]
        private void Construct(MatchingUserView matchingUserView, SeController seController)
        {
            _matchingUserView = matchingUserView;
            _seController = seController;
        }

        public override GameState GetState() => GameState.Ready;

        public override UniTask InitAsync(CancellationToken token)
        {
            countDownText.text = "";
            return base.InitAsync(token);
        }

        public override UniTask ResetAsync(GameState state, CancellationToken token)
        {
            return base.ResetAsync(state, token);
        }

        public override async UniTask<GameState> TickAsync(CancellationToken token)
        {
            await _matchingUserView.HidePlayerNameAsync(token);

            await StartCountDownAsync(token);

            return GameState.Battle;
        }

        public override UniTask DisposeAsync(CancellationToken token)
        {
            return base.DisposeAsync(token);
        }

        private async UniTask StartCountDownAsync(CancellationToken token)
        {
            countDownText.text = "Ready...?";
            await UniTask.Delay(TimeSpan.FromSeconds(AnimationTime.READY), cancellationToken: token);

            _seController.Play(SeType.Start);
            countDownText.text = "Start!!";
            await countDownText.rectTransform
                .DOShakeScale(AnimationTime.UI_MOVE)
                .SetEase(Ease.Linear)
                .WithCancellation(token);

            countDownText.text = "";
        }
    }
}