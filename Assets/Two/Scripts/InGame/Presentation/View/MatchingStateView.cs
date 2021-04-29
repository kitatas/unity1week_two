using System;
using TMPro;
using Two.InGame.Application;
using UnityEngine;

namespace Two.InGame.Presentation.View
{
    public sealed class MatchingStateView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI matchingStateText = default;

        public void Show(MatchingState matchingState)
        {
            switch (matchingState)
            {
                case MatchingState.None:
                    matchingStateText.text = "";
                    break;
                case MatchingState.Connecting:
                    matchingStateText.text = "対戦の準備中です。";
                    break;
                case MatchingState.Matching:
                    matchingStateText.text = "対戦相手を探しています。";
                    break;
                case MatchingState.Matched:
                    matchingStateText.text = "対戦相手が見つかりました。";
                    break;
                case MatchingState.Disconnected:
                    matchingStateText.text = "通信が切断されました。";
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(matchingState), matchingState, null);
            }
        }
    }
}