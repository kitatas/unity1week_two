using TMPro;
using UnityEngine;

namespace Two.InGame.Presentation.View
{
    public sealed class HpView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI hpText = default;

        public void Show(int value)
        {
            hpText.text = $"{value}";
        }
    }
}