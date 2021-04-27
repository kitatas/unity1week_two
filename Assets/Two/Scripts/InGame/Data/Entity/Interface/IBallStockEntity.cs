using Two.InGame.Presentation.View.Interface;

namespace Two.InGame.Data.Entity.Interface
{
    public interface IBallStockEntity
    {
        int GetStockCount();
        bool IsStockFull();
        bool IsStockEmpty();
        void Increase(IBallView ballView);
        IBallView Decrease();
    }
}