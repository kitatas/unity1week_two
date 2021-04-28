using Two.InGame.Application;
using Two.InGame.Domain.Model.Interface;
using UniRx;

namespace Two.InGame.Domain.Model
{
    public sealed class MatchingStateModel : IMatchingStateModel
    {
        private readonly ReactiveProperty<MatchingState> _matchingState;

        public MatchingStateModel()
        {
            _matchingState = new ReactiveProperty<MatchingState>(GameParam.INIT_MATCHING_STATE);
        }

        public IReadOnlyReactiveProperty<MatchingState> matchingState => _matchingState;

        public void SetState(MatchingState value)
        {
            _matchingState.Value = value;
        }
    }
}