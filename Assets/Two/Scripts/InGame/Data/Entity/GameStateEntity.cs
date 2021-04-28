using Two.InGame.Application;
using Two.InGame.Data.Entity.Interface;

namespace Two.InGame.Data.Entity
{
    public sealed class GameStateEntity : IGameStateEntity
    {
        private GameState _gameState;

        public GameStateEntity()
        {
            _gameState = GameParam.INIT_GAME_STATE;
        }

        public GameState GetState() => _gameState;

        public void SetState(GameState value) => _gameState = value;
    }
}