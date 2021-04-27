using Two.InGame.Data.Entity.Interface;
using Two.InGame.Domain.Model.Interface;
using Two.InGame.Domain.UseCase.Interface;
using Two.InGame.Presentation.View.Interface;

namespace Two.InGame.Domain.UseCase
{
    public sealed class BallStockUseCase : IBallStockUseCase
    {
        private readonly IBallStockEntity _ballStockEntity;
        private readonly IBallStockModel _ballStockModel;

        public BallStockUseCase(IBallStockEntity ballStockEntity, IBallStockModel ballStockModel)
        {
            _ballStockEntity = ballStockEntity;
            _ballStockModel = ballStockModel;
        }

        public void PickUp(IBallView ballView)
        {
            if (_ballStockEntity.IsStockFull())
            {
                return;
            }

            _ballStockEntity.Increase(ballView);
            _ballStockModel.SetStockCount(_ballStockEntity.GetStockCount());
        }

        public void Shot()
        {
            if (_ballStockEntity.IsStockEmpty())
            {
                return;
            }

            var ball = _ballStockEntity.Decrease();
            ball.SetOwner(null);
            _ballStockModel.SetStockCount(_ballStockEntity.GetStockCount());
        }
    }
}