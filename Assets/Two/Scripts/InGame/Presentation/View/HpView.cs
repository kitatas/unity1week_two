using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Two.InGame.Presentation.View
{
    public sealed class HpView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI hpText = default;
        [SerializeField] private List<Image> images = default;
        private readonly Color _activateColor = Color.magenta;
        private readonly Color _deactivateColor = Color.gray;

        public void Show(int value)
        {
            for (int i = 0; i < images.Count; i++)
            {
                images[i].color = i < value ? _activateColor : _deactivateColor;
            }
        }
    }
}