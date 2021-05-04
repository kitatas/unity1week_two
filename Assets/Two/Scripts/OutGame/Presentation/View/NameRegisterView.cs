using System;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Two.OutGame.Presentation.View
{
    public sealed class NameRegisterView : MonoBehaviour
    {
        [SerializeField] private TMP_InputField nameField = default;
        [SerializeField] private Button registerButton = default;

        [SerializeField] private TextMeshProUGUI noticeText = default;
        [SerializeField] private Button backButton = default;

        public IObservable<Unit> RegisterAsObservable() => registerButton.OnClickAsObservable();

        public string GetInputName() => nameField.text;

        public void SetName(string playerName)
        {
            nameField.text = playerName;
        }

        public void Registering()
        {
            noticeText.text = $"登録しています。";
            backButton.gameObject.SetActive(false);
        }

        public void Registered()
        {
            noticeText.text = $"登録しました。";
            backButton.gameObject.SetActive(true);
        }
    }
}