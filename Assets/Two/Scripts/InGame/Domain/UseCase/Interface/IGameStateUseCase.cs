using Two.InGame.Application;
using UniRx;

namespace Two.InGame.Domain.UseCase.Interface
{
    public interface IGameStateUseCase
    {
        IReadOnlyReactiveProperty<GameState> state { get; }
        void SetState(GameState value);
        bool IsEqual(GameState value);
    }
}