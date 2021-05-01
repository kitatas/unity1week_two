using Two.Common.Application;
using Two.Common.Domain.UseCase.Interface;
using VContainer;

namespace Two.Common.Presentation.Controller.Sound
{
    public sealed class BgmController : BaseAudioSource
    {
        private IBgmUseCase _bgmUseCase;

        [Inject]
        private void Construct(IBgmUseCase bgmUseCase)
        {
            _bgmUseCase = bgmUseCase;
        }

        public void Play(BgmType bgmType)
        {
            var clip = _bgmUseCase.FindClip(bgmType);

            if (clip == null)
            {
                return;
            }

            if (clip == audioSource.clip)
            {
                return;
            }

            audioSource.clip = clip;
            audioSource.Play();
        }
    }
}