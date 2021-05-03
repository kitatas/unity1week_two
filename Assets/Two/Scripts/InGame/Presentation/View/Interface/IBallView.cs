using Two.InGame.Application;
using UnityEngine;

namespace Two.InGame.Presentation.View.Interface
{
    public interface IBallView
    {
        PlayerType GetOwnerType();
        void PickUp(Transform owner, PlayerType playerType);
        void Shot();
    }
}