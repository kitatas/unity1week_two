using TMPro;
using Two.OutGame.Domain.UseCase.Interface;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Two.OutGame.Presentation.View
{
    public sealed class NameRegisterView : MonoBehaviour
    {
        [SerializeField] private TMP_InputField nameField = default;
        [SerializeField] private Button resistButton = default;

        private INameRegisterUseCase _nameRegisterUseCase;

        [Inject]
        private void Construct(INameRegisterUseCase nameRegisterUseCase)
        {
            _nameRegisterUseCase = nameRegisterUseCase;
        }

        private void Start()
        {
            nameField.text = _nameRegisterUseCase.LoadName();

            resistButton
                .OnClickAsObservable()
                .Subscribe(_ =>
                {
                    if (string.IsNullOrWhiteSpace(nameField.text))
                    {
                        return;
                    }

                    _nameRegisterUseCase.SaveName(nameField.text);
                })
                .AddTo(this);
        }
    }
}