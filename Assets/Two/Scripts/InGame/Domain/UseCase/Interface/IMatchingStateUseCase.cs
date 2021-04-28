using Two.InGame.Application;
using UniRx;

namespace Two.InGame.Domain.UseCase.Interface
{
    public interface IMatchingStateUseCase
    {
        IReadOnlyReactiveProperty<MatchingState> state { get; }
        void SetState(MatchingState value);
    }
}