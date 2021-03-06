using Two.InGame.Domain.UseCase.Interface;
using UnityEngine;

namespace Two.InGame.Domain.UseCase
{
    public sealed class KeyboardInputProvider : IInputProvider
    {
        public float horizontal => Input.GetAxisRaw("Horizontal");
        public float vertical => Input.GetAxisRaw("Vertical");
        public bool isAttack => Input.GetMouseButtonDown(0);
        public Vector3 mousePosition => Input.mousePosition;
    }
}