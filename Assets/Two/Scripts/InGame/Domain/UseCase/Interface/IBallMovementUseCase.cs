using UnityEngine;

namespace Two.InGame.Domain.UseCase.Interface
{
    public interface IBallMovementUseCase
    {
        void Shot(Vector3 shotDirection);
        void ResetVelocity();
        Vector3 GetVelocity();
    }
}