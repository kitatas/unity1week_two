using Two.InGame.Application;

namespace Two.InGame.Domain.UseCase.Interface
{
    public interface IMatchingUseCase
    {
        PlayerType GetPlayerType();
        PlayerType GetEnemyType();
        string GetPlayerName();
        string GetEnemyName();
        int GetPlayerRate();
        int GetEnemyRate();
        void SetWinner(PlayerType type);
        string GetUserName(PlayerType type);
        int GetUserRate(PlayerType type);
        string GetWinnerName();
        bool IsWin();
        int GetAddRateValue();
    }
}