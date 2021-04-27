using Two.InGame.Presentation.View.Interface;

namespace Two.InGame.Domain.UseCase.Interface
{
    public interface IBallStockUseCase
    {
        void PickUp(IBallView ballView);
        void Shot();
    }
}