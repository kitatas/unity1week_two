using Two.InGame.Application;

namespace Two.InGame.Data.Entity.Interface
{
    public interface IMatchingEntity
    {
        void SetMatchingUserName(string player, string enemy);
        void SetPlayerType(PlayerType type);
    }
}