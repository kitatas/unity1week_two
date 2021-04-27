using Two.InGame.Presentation.Controller;

namespace Two.InGame.Presentation.View.Interface
{
    public interface IBallView
    {
        PlayerController GetOwner();
        void SetOwner(PlayerController owner);
    }
}