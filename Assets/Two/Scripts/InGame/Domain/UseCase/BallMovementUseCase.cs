using Two.InGame.Domain.UseCase.Interface;
using UnityEngine;

namespace Two.InGame.Domain.UseCase
{
    public sealed class BallMovementUseCase : IBallMovementUseCase
    {
        private readonly float _shotPower = 25.0f;
        private readonly Rigidbody _rigidbody;

        public BallMovementUseCase(Rigidbody rigidbody)
        {
            _rigidbody = rigidbody;
        }

        public void Shot(Vector3 shotDirection)
        {
            _rigidbody.AddForce(shotDirection * _shotPower, ForceMode.VelocityChange);
        }

        public void ResetVelocity() => _rigidbody.velocity = Vector3.zero;

        public Vector3 GetVelocity() => _rigidbody.velocity;
    }
}