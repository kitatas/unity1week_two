using Two.Common.Domain.Repository.Interface;
using Two.OutGame.Domain.UseCase.Interface;

namespace Two.OutGame.Domain.UseCase
{
    public sealed class NameRegisterUseCase : INameRegisterUseCase
    {
        private readonly INameRepository _nameRepository;

        public NameRegisterUseCase(INameRepository nameRepository)
        {
            _nameRepository = nameRepository;
        }

        public string LoadName() => _nameRepository.Load();

        public void SaveName(string name) => _nameRepository.Save(name);
    }
}