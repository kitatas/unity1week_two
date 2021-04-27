using Two.InGame.Application;
using UnityEngine;

namespace Two.InGame.Presentation.View.Interface
{
    public interface IBallView
    {
        PlayerType GetOwnerType();
        void SetOwner(Transform owner, PlayerType playerType);
        void Shot();
    }
}