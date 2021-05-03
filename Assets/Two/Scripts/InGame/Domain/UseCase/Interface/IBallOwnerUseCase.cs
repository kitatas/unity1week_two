using Two.InGame.Application;
using UnityEngine;

namespace Two.InGame.Domain.UseCase.Interface
{
    public interface IBallOwnerUseCase
    {
        void ResetOwner();
        void ResetOwnerType();
        void SetOwner(Transform owner, PlayerType playerType);
        Transform GetOwner();
        Vector3 GetOwnerPosition();
        Vector3 GetOwnerForward();
        PlayerType GetOwnerType();
    }
}