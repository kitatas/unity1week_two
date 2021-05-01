using System;
using Two.Common.Application;
using Two.Common.Domain.UseCase.Interface;
using VContainer;

namespace Two.Common.Presentation.Controller.Sound
{
    public sealed class SeController : BaseAudioSource
    {
        private ISeUseCase _seUseCase;

        [Inject]
        private void Construct(ISeUseCase seUseCase)
        {
            _seUseCase = seUseCase;
        }

        public void Play(SeType seType)
        {
            var clip = _seUseCase.FindClip(seType);

            if (clip == null)
            {
                return;
            }

            audioSource.PlayOneShot(clip);
        }

        public void Play(ButtonType buttonType)
        {
            Play(GetButtonSeType(buttonType));
        }

        private static SeType GetButtonSeType(ButtonType buttonType)
        {
            switch (buttonType)
            {
                case ButtonType.Decision:
                    return SeType.Decision;
                case ButtonType.Cancel:
                    return SeType.Cancel;
                default:
                    throw new ArgumentOutOfRangeException(nameof(buttonType), buttonType, null);
            }
        }
    }
}