using UnityEngine;

namespace Two.InGame.Domain.UseCase.Interface
{
    public interface IRotationUseCase
    {
        void Rotate(Vector3 mousePosition);
    }
}