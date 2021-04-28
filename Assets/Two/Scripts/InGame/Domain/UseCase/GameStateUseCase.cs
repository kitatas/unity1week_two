using Two.InGame.Application;
using Two.InGame.Data.Entity.Interface;
using Two.InGame.Domain.Model.Interface;
using Two.InGame.Domain.UseCase.Interface;
using UniRx;

namespace Two.InGame.Domain.UseCase
{
    public sealed class GameStateUseCase : IGameStateUseCase
    {
        private readonly IGameStateEntity _gameStateEntity;
        private readonly IGameStateModel _gameStateModel;

        public GameStateUseCase(IGameStateEntity gameStateEntity, IGameStateModel gameStateModel)
        {
            _gameStateEntity = gameStateEntity;
            _gameStateModel = gameStateModel;
        }

        public IReadOnlyReactiveProperty<GameState> state => _gameStateModel.gameState;

        public void SetState(GameState value)
        {
            _gameStateEntity.SetState(value);
            _gameStateModel.SetState(_gameStateEntity.GetState());
        }

        public bool IsEqual(GameState value) => _gameStateEntity.GetState() == value;
    }
}