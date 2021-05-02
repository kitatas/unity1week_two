using Two.InGame.Presentation.View.Interface;

namespace Two.InGame.Domain.UseCase.Interface
{
    public interface IBallStockUseCase
    {
        void Push(IBallView ballView);
        IBallView Pop();
    }
}