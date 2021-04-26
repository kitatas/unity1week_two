using Two.InGame.Data.Entity.Interface;
using Two.InGame.Domain.UseCase.Interface;
using UnityEngine;

namespace Two.InGame.Domain.UseCase
{
    public sealed class BallStockUseCase : IBallStockUseCase
    {
        private readonly IBallStockEntity _ballStockEntity;

        public BallStockUseCase(IBallStockEntity ballStockEntity)
        {
            _ballStockEntity = ballStockEntity;
        }

        public void PickUp()
        {
            if (_ballStockEntity.IsStockFull())
            {
                return;
            }

            _ballStockEntity.Increase();
        }

        public void Shot()
        {
            if (_ballStockEntity.IsStockEmpty())
            {
                return;
            }

            _ballStockEntity.Decrease();
        }
    }
}