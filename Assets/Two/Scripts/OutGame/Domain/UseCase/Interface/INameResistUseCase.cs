namespace Two.OutGame.Domain.UseCase.Interface
{
    public interface INameResistUseCase
    {
        string LoadName();
        void SaveName(string name);
    }
}