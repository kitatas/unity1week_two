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

        public IObservable<Unit> RegisterAsObservable() => registerButton.OnClickAsObservable();

        public string GetInputName() => nameField.text;

        public void SetName(string playerName)
        {
            nameField.text = playerName;
        }
    }
}