using Two.InGame.Application;
using UniRx;

namespace Two.InGame.Domain.Model.Interface
{
    public interface IMatchingStateModel
    {
        IReadOnlyReactiveProperty<MatchingState> matchingState { get; }
        void SetState(MatchingState value);
    }
}