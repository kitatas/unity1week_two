namespace Two.InGame.Domain.UseCase.Interface
{
    public interface IInputProvider
    {
        float horizontal { get; }
        float vertical { get; }
        bool isAttack { get; }
    }
}