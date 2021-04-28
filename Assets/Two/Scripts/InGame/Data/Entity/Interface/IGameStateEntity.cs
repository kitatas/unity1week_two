using Two.InGame.Application;

namespace Two.InGame.Data.Entity.Interface
{
    public interface IGameStateEntity
    {
        GameState GetState();
        void SetState(GameState value);
    }
}