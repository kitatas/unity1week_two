using Two.Common.Application;
using Two.Common.Domain.Repository.Interface;
using Two.Common.Domain.UseCase.Interface;
using UnityEngine;

namespace Two.Common.Domain.UseCase
{
    public sealed class SoundUseCase : IBgmUseCase, ISeUseCase
    {
        private readonly ISoundRepository _soundRepository;

        public SoundUseCase(ISoundRepository soundRepository)
        {
            _soundRepository = soundRepository;
        }

        public AudioClip FindClip(BgmType type)
        {
            var data = _soundRepository.bgmData
                .Find(x => x.type == type);

            return data.clip;
        }

        public AudioClip FindClip(SeType type)
        {
            var data = _soundRepository.seData
                .Find(x => x.type == type);

            return data.clip;
        }
    }
}