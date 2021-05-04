using Two.InGame.Application;

namespace Two.InGame.Data.Entity.Interface
{
    public interface IMatchingEntity
    {
        void SetPlayerData(string playerName, int playerRate);
        void SetEnemyData(string enemyName, int enemyRate);
        void SetMatchingUserType(PlayerType player, PlayerType enemy);
        string GetPlayerName();
        string GetEnemyName();
        PlayerType GetPlayerType();
        PlayerType GetEnemyType();
        int GetPlayerRate();
        int GetEnemyRate();
    }
}