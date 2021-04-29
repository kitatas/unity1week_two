using Two.InGame.Application;
using Two.InGame.Data.Entity.Interface;
using Two.InGame.Domain.UseCase.Interface;
using UnityEngine;

namespace Two.InGame.Domain.UseCase
{
    public sealed class MatchingUseCase : IMatchingUseCase
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

        public string GetWinnerName()
        {
            if (_winner == GetPlayerType())
            {
                return GetPlayerName();
            }

            if (_winner == GetEnemyType())
            {
                return GetEnemyName();
            }

            return $"{PlayerType.None}";
        }
    }
}