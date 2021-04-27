namespace Two.InGame.Domain.UseCase.Interface
{
    public interface IHpUseCase
    {
        void Damage();
        bool IsDead();
    }
}