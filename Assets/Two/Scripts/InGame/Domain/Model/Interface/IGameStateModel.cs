using Two.InGame.Application;
using UniRx;

namespace Two.InGame.Domain.Model.Interface
{
    public interface IGameStateModel
    {
        IReadOnlyReactiveProperty<GameState> gameState { get; }
        void SetState(GameState value);
    }
}