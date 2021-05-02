using Two.Common.Application;
using Two.Common.Presentation.Controller.Sound;
using UnityEngine;
using VContainer;

namespace Two.OutGame.Presentation.Controller
{
    public sealed class TitleSequencer : MonoBehaviour
    {
        private BgmController _bgmController;

        [Inject]
        private void Construct(BgmController bgmController)
        {
            _bgmController = bgmController;
        }

        private void Awake()
        {
            _bgmController.Play(BgmType.Main);
        }
    }
}