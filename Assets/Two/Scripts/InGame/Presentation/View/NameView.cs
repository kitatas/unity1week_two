using TMPro;
using UnityEngine;

namespace Two.InGame.Presentation.View
{
    public sealed class NameView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI nameText = default;

        public void Init(string playerName)
        {
            nameText.text = $"{playerName}";
        }
    }
}