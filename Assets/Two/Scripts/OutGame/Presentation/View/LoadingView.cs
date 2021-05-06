using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Two.OutGame.Presentation.View
{
    public sealed class LoadingView : MonoBehaviour
    {
        [SerializeField] private Image loadImage = default;
        [SerializeField] private TextMeshProUGUI loadingText = default;

        public void Init()
        {
            var animator = new DOTweenTMPAnimator(loadingText);
            var offset = Vector3.up * 10.0f;
            for (int i = 0; i < animator.textInfo.characterCount; i++)
            {
                var currentOffset = animator.GetCharOffset(i);
                animator
                    .DOOffsetChar(i, currentOffset + offset, 0.2f)
                    .SetEase(Ease.OutFlash, 2)
                    .SetDelay(0.05f * i);
            }
        }

        public void Activate(bool value)
        {
            loadImage.gameObject.SetActive(value);
        }
    }
}