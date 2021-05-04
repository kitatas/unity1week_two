using TMPro;
using UnityEngine;

namespace Two.OutGame.Presentation.View
{
    public sealed class RatingView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI nameText = default;
        [SerializeField] private TextMeshProUGUI ratingText = default;

        public void SetPlayerInfo(string playerName, string playerRate)
        {
            nameText.text = playerName;
            ratingText.text = playerRate;
        }
    }
}