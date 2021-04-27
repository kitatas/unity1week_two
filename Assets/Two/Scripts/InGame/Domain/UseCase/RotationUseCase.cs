using Two.InGame.Domain.UseCase.Interface;
using UnityEngine;

namespace Two.InGame.Domain.UseCase
{
    public sealed class RotationUseCase : IRotationUseCase
    {
        private readonly Camera _camera;
        private readonly Transform _transform;
        private readonly float _height;
        private Vector3 _rotatePoint;

        public RotationUseCase(Camera camera, Transform transform)
        {
            _camera = camera;
            _transform = transform;
            _height = _transform.position.y;
            _rotatePoint = new Vector3(0.0f, _height, 0.0f);
        }

        public void Rotate(Vector3 mousePosition)
        {
            _rotatePoint = GetHitRayPosition(mousePosition);
            _transform.LookAt(_rotatePoint);
        }

        private Vector3 GetHitRayPosition(Vector3 mousePosition)
        {
            var ray = _camera.ScreenPointToRay(mousePosition);
            if (Physics.Raycast(ray, out var hit))
            {
                var hitPosition = hit.point;
                hitPosition.y = _height;
                return hitPosition;
            }

            return _rotatePoint;
        }
    }
}