using Two.InGame.Application;
using Two.InGame.Domain.Model.Interface;
using UniRx;

namespace Two.InGame.Domain.Model
{
    public sealed class GameStateModel : IGameStateModel
    {
        private readonly ReactiveProperty<GameState> _gameState;

        public GameStateModel()
        {
            _gameState = new ReactiveProperty<GameState>(GameParam.INIT_GAME_STATE);
        }

        public IReadOnlyReactiveProperty<GameState> gameState => _gameState;

        public void SetState(GameState value)
        {
            _gameState.Value = value;
        }
    }
}