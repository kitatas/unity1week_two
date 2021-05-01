using Two.Common.Application;
using Two.Common.Presentation.Controller.Sound;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Two.Common.Presentation.View
{
    [RequireComponent(typeof(Button))]
    public sealed class ButtonView : MonoBehaviour
    {
        [SerializeField] private ButtonType type = default;

        private SeController _seController;

        [Inject]
        private void Construct(SeController seController)
        {
            _seController = seController;
        }

        private void Start()
        {
            GetComponent<Button>()
                .OnClickAsObservable()
                .Subscribe(_ => _seController.Play(type))
                .AddTo(this);
        }
    }
}