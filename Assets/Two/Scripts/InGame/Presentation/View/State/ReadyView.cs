using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using TMPro;
using Two.InGame.Application;
using UnityEngine;
using VContainer;

namespace Two.InGame.Presentation.View.State
{
    public sealed class ReadyView : BaseState
    {
        [SerializeField] private TextMeshProUGUI countDownText = default;

        private MatchingUserView _matchingUserView;

        [Inject]
        private void Construct(MatchingUserView matchingUserView)
        {
            _matchingUserView = matchingUserView;
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
            countDownText.text = "3";
            await UniTask.Delay(TimeSpan.FromSeconds(1.0f), cancellationToken: token);

            countDownText.text = "2";
            await UniTask.Delay(TimeSpan.FromSeconds(1.0f), cancellationToken: token);

            countDownText.text = "1";
            await UniTask.Delay(TimeSpan.FromSeconds(1.0f), cancellationToken: token);

            countDownText.text = "Start!!";
            await UniTask.Delay(TimeSpan.FromSeconds(0.5f), cancellationToken: token);

            countDownText.text = "";
        }
    }
}