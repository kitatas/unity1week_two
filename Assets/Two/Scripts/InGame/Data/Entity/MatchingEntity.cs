using Two.InGame.Application;
using Two.InGame.Data.Entity.Interface;

namespace Two.InGame.Data.Entity
{
    public sealed class MatchingEntity : IMatchingEntity
    {
        private string _playerName;
        private string _enemyName;
        private PlayerType _playerType;
        private PlayerType _enemyType;
        private int _playerRate;
        private int _enemyRate;

        public void SetPlayerData(string playerName, int playerRate)
        {
            _playerName = playerName;
            _playerRate = playerRate;
        }
        
        public void SetEnemyData(string enemyName, int enemyRate)
        {
            _enemyName = enemyName;
            _enemyRate = enemyRate;
        }

        public void SetMatchingUserType(PlayerType player, PlayerType enemy)
        {
            _playerType = player;
            _enemyType = enemy;
        }

        public string GetPlayerName() => _playerName;

        public string GetEnemyName() => _enemyName;

        public PlayerType GetPlayerType() => _playerType;

        public PlayerType GetEnemyType() => _enemyType;

        public int GetPlayerRate() => _playerRate;

        public int GetEnemyRate() => _enemyRate;
    }
}