using Two.InGame.Application;
using Two.InGame.Data.Entity.Interface;

namespace Two.InGame.Data.Entity
{
    public sealed class MatchingStateEntity : IMatchingStateEntity
    {
        private MatchingState _matchingState;

        public MatchingStateEntity()
        {
            _matchingState = GameParam.INIT_MATCHING_STATE;
        }

        public MatchingState GetState() => _matchingState;

        public void SetState(MatchingState value) => _matchingState = value;
    }
}