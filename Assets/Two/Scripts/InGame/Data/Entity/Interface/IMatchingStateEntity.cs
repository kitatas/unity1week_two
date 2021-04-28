using Two.InGame.Application;

namespace Two.InGame.Data.Entity.Interface
{
    public interface IMatchingStateEntity
    {
        MatchingState GetState();
        void SetState(MatchingState value);
    }
}