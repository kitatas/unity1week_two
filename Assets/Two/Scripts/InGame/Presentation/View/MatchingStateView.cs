using System;
using TMPro;
using Two.Common.Application;
using Two.Common.Presentation.Controller.Sound;
using Two.InGame.Application;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Two.InGame.Presentation.View
{
    public sealed class MatchingStateView : MonoBehaviour
    {
        [SerializeField] private Image background = default;
        [SerializeField] private TextMeshProUGUI matchingStateText = default;

        private SeController _seController;

        [Inject]
        private void Construct(SeController seController)
        {
            _seController = seController;
        }

        public void Show(MatchingState matchingState)
        {
            switch (matchingState)
            {
                case MatchingState.None:
                    background.enabled = false;
                    matchingStateText.text = "";
                    break;
                case MatchingState.Connecting:
                    background.enabled = true;
                    matchingStateText.text = "対戦の準備中です。";
                    break;
                case MatchingState.Matching:
                    background.enabled = true;
                    _seController.Play(SeType.Notice);
                    matchingStateText.text = "対戦相手を探しています。";
                    break;
                case MatchingState.Matched:
                    background.enabled = true;
                    _seController.Play(SeType.Notice);
                    matchingStateText.text = "対戦相手が見つかりました。";
                    break;
                case MatchingState.Disconnected:
                    background.enabled = true;
                    _seController.Play(SeType.Alert);
                    matchingStateText.text = "通信が切断されました。";
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(matchingState), matchingState, null);
            }
        }
    }
}