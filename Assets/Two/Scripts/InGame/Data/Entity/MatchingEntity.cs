using Two.InGame.Application;
using Two.InGame.Data.Entity.Interface;

namespace Two.InGame.Data.Entity
{
    public sealed class MatchingEntity : IMatchingEntity
    {
        private string _playerName;
        private string _enemyName;
        private PlayerType _playerType;

        public void SetMatchingUserName(string player, string enemy)
        {
            _playerName = player;
            _enemyName = enemy;
        }

        public void SetPlayerType(PlayerType type)
        {
            _playerType = type;
        }
    }
}