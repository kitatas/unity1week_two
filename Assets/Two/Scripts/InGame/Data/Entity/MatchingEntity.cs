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

        public void SetMatchingUserName(string player, string enemy)
        {
            _playerName = player;
            _enemyName = enemy;
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
    }
}