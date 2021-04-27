using System.Collections.Generic;
using Two.InGame.Application;
using Two.InGame.Data.Entity.Interface;
using Two.InGame.Presentation.View.Interface;

namespace Two.InGame.Data.Entity
{
    public sealed class BallStockEntity : IBallStockEntity
    {
        private readonly Stack<IBallView> _ballViews;

        public BallStockEntity()
        {
            _ballViews = new Stack<IBallView>();
        }

        public int GetStockCount() => _ballViews.Count;

        public bool IsStockFull() => GetStockCount() >= PlayerParam.MAX_STOCK_COUNT;

        public bool IsStockEmpty() => GetStockCount() <= 0;

        public void Increase(IBallView ballView) => _ballViews.Push(ballView);

        public IBallView Decrease() => _ballViews.Pop();
    }
}