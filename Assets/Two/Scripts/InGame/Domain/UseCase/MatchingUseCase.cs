using Two.Common.Application;
using Two.InGame.Application;
using Two.InGame.Data.Entity.Interface;
using Two.InGame.Domain.UseCase.Interface;

namespace Two.InGame.Domain.UseCase
{
    public sealed class MatchingUseCase : IMatchingUseCase, ITweetUseCase
    {
        private PlayerType _winner;
        private readonly IMatchingEntity _matchingEntity;

        public MatchingUseCase(IMatchingEntity matchingEntity)
        {
            _winner = PlayerType.None;
            _matchingEntity = matchingEntity;
        }

        public PlayerType GetPlayerType() => _matchingEntity.GetPlayerType();

        public PlayerType GetEnemyType() => _matchingEntity.GetEnemyType();

        public string GetPlayerName() => _matchingEntity.GetPlayerName();

        public string GetEnemyName() => _matchingEntity.GetEnemyName();

        public void SetWinner(PlayerType type)
        {
            _winner = type;
        }

        public string GetUserName(PlayerType type)
        {
            if (type == GetPlayerType())
            {
                return GetPlayerName();
            }

            if (type == GetEnemyType())
            {
                return GetEnemyName();
            }

            return $"{PlayerType.None}";
        }

        public string GetWinnerName()
        {
            return GetUserName(_winner);
        }

        public string GetTweetText()
        {
            return GetTweetMainText() + GetHashTag();
        }

        private string GetTweetMainText()
        {
            if (_winner == GetPlayerType())
            {
                return $"{GetEnemyName()}さんに勝利した！\n";
            }

            if (_winner == GetEnemyType())
            {
                return $"{GetEnemyName()}さんに敗北した...\n";
            }

            return "";
        }

        private static string GetHashTag()
        {
            return $"#{GameConfig.HASH_TAG1} #{GameConfig.HASH_TAG2} #{GameConfig.GAME_ID}\n";
        }
    }
}