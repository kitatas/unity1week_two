using TMPro;
using Two.OutGame.Domain.UseCase.Interface;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Two.OutGame.Presentation.View
{
    public sealed class NameResistView : MonoBehaviour
    {
        [SerializeField] private TMP_InputField nameField = default;
        [SerializeField] private Button resistButton = default;

        private INameResistUseCase _nameResistUseCase;

        [Inject]
        private void Construct(INameResistUseCase nameResistUseCase)
        {
            _nameResistUseCase = nameResistUseCase;
        }

        private void Start()
        {
            nameField.text = _nameResistUseCase.LoadName();

            resistButton
                .OnClickAsObservable()
                .Subscribe(_ =>
                {
                    if (string.IsNullOrWhiteSpace(nameField.text))
                    {
                        return;
                    }

                    _nameResistUseCase.SaveName(nameField.text);
                })
                .AddTo(this);
        }
    }
}