using Two.InGame.Application;
using Two.InGame.Domain.UseCase.Interface;
using UnityEngine;

namespace Two.InGame.Domain.UseCase
{
    public sealed class BallOwnerUseCase : IBallOwnerUseCase
    {
        private Transform _owner;
        private PlayerType _ownerType;

        public BallOwnerUseCase()
        {
            ResetOwner();
            ResetOwnerType();
        }

        public void ResetOwner() => _owner = null;

        public void ResetOwnerType() => _ownerType = PlayerType.None;

        public void SetOwner(Transform owner, PlayerType playerType)
        {
            _owner = owner;
            _ownerType = playerType;
        }

        public Transform GetOwner() => _owner;

        public Vector3 GetOwnerPosition() => GetOwner().position;

        public Vector3 GetOwnerForward() => GetOwner().forward;

        public PlayerType GetOwnerType() => _ownerType;
    }
}