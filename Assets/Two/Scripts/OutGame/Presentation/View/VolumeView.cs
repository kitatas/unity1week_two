using Two.Common.Application;
using Two.Common.Presentation.Controller.Sound;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Two.OutGame.Presentation.View
{
    public sealed class VolumeView : MonoBehaviour
    {
        [SerializeField] private Slider bgmSlider = default;
        [SerializeField] private Slider seSlider = default;

        private BgmController _bgmController;
        private SeController _seController;

        [Inject]
        private void Construct(BgmController bgmController, SeController seController)
        {
            _bgmController = bgmController;
            _seController = seController;
        }

        private void Start()
        {
            seSlider
                .OnPointerUpAsObservable()
                .Subscribe(_ => _seController.Play(ButtonType.Decision))
                .AddTo(seSlider);

            bgmSlider.value = _bgmController.GetVolume();
            bgmSlider
                .OnValueChangedAsObservable()
                .Subscribe(_bgmController.SetVolume)
                .AddTo(bgmSlider);

            seSlider.value = _seController.GetVolume();
            seSlider
                .OnValueChangedAsObservable()
                .Subscribe(_seController.SetVolume)
                .AddTo(seSlider);
        }
    }
}