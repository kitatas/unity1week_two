using Two.InGame.Application;

namespace Two.InGame.Data.Entity.Interface
{
    public interface IMatchingEntity
    {
        void SetMatchingUserName(string player, string enemy);
        void SetMatchingUserType(PlayerType player, PlayerType enemy);
        string GetPlayerName();
        string GetEnemyName();
        PlayerType GetPlayerType();
        PlayerType GetEnemyType();
    }
}