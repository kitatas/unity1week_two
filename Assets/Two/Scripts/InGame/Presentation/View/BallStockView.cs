using TMPro;
using UnityEngine;

namespace Two.InGame.Presentation.View
{
    public sealed class BallStockView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI stockCountText = default;

        public void Show(int count)
        {
            stockCountText.text = $"{count}";
        }
    }
}