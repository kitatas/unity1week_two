using Two.InGame.Application;
using Two.InGame.Data.Entity.Interface;

namespace Two.InGame.Data.Entity
{
    public sealed class BallStockEntity : IBallStockEntity
    {
        private int _stockCount;

        public BallStockEntity()
        {
            _stockCount = 0;
        }

        public int GetStockCount() => _stockCount;

        public bool IsStockFull() => GetStockCount() >= PlayerParam.MAX_STOCK_COUNT;

        public bool IsStockEmpty() => GetStockCount() <= 0;

        public void Increase() => _stockCount++;

        public void Decrease() => _stockCount--;
    }
}