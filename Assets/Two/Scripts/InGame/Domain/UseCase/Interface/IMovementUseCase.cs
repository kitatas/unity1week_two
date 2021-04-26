using UnityEngine;

namespace Two.InGame.Domain.UseCase.Interface
{
    public interface IMovementUseCase
    {
        void Move(Vector3 moveVector);
    }
}