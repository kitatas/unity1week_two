using Two.InGame.Domain.UseCase.Interface;
using UnityEngine;

namespace Two.InGame.Domain.UseCase
{
    public sealed class BallColliderUseCase : IBallColliderUseCase
    {
        private readonly Collider _collider;

        public BallColliderUseCase(Collider collider)
        {
            _collider = collider;
        }

        public void SetTrigger(bool value) => _collider.isTrigger = value;
    }
}