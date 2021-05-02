using Two.InGame.Domain.UseCase.Interface;
using UnityEngine;

namespace Two.InGame.Domain.UseCase
{
    public sealed class MovementUseCase : IMovementUseCase
    {
        private readonly float _moveSpeed = 500.0f;
        private readonly Rigidbody _rigidbody;

        public MovementUseCase(Rigidbody rigidbody)
        {
            _rigidbody = rigidbody;
        }

        public void Move(Vector3 moveVector)
        {
            _rigidbody.velocity = moveVector * _moveSpeed * Time.deltaTime;
        }

        public Vector3 GetVelocity() => _rigidbody.velocity;
    }
}