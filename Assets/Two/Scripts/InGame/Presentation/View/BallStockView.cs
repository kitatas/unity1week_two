using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Two.InGame.Presentation.View
{
    public sealed class BallStockView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI stockCountText = default;
        [SerializeField] private List<Image> images = default;
        private readonly Color _activateColor = Color.cyan;
        private readonly Color _deactivateColor = Color.gray;

        public void Show(int count)
        {
            for (int i = 0; i < images.Count; i++)
            {
                images[i].color = i < count ? _activateColor : _deactivateColor;
            }
        }
    }
}