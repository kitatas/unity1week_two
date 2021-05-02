using Two.Common.Application;
using Two.Common.Presentation.Controller.Sound;
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
        private readonly SeController _seController;

            public BallStockUseCase(IBallStockEntity ballStockEntity, IBallStockModel ballStockModel, SeController seController)
        {
            _ballStockEntity = ballStockEntity;
            _ballStockModel = ballStockModel;
            _seController = seController;
        }

        public void Push(IBallView ballView)
        {
            if (_ballStockEntity.IsStockFull())
            {
                return;
            }

            _seController.Play(SeType.PickUp);
            _ballStockEntity.Increase(ballView);
            _ballStockModel.SetStockCount(_ballStockEntity.GetStockCount());
        }

        public IBallView Pop()
        {
            if (_ballStockEntity.IsStockEmpty())
            {
                return null;
            }

            _seController.Play(SeType.Shot);
            var ball = _ballStockEntity.Decrease();
            _ballStockModel.SetStockCount(_ballStockEntity.GetStockCount());
            return ball;
        }
    }
}