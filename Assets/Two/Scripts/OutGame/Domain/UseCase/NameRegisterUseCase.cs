using Two.Common.Domain.Repository.Interface;
using Two.OutGame.Domain.UseCase.Interface;

namespace Two.OutGame.Domain.UseCase
{
    public sealed class NameRegisterUseCase : INameRegisterUseCase
    {
        private readonly IPlayerDataRepository _playerDataRepository;

        public NameRegisterUseCase(IPlayerDataRepository playerDataRepository)
        {
            _playerDataRepository = playerDataRepository;
        }

        public string LoadName() => _playerDataRepository.LoadName();

        public void SaveName(string name) => _playerDataRepository.SaveName(name);
    }
}