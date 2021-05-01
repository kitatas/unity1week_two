namespace Two.OutGame.Domain.UseCase.Interface
{
    public interface INameRegisterUseCase
    {
        string LoadName();
        void SaveName(string name);
    }
}