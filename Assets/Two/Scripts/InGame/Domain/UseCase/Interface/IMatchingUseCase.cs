using Two.InGame.Application;

namespace Two.InGame.Domain.UseCase.Interface
{
    public interface IMatchingUseCase
    {
        PlayerType GetPlayerType();
        PlayerType GetEnemyType();
        string GetPlayerName();
        string GetEnemyName();
        void SetWinner(PlayerType type);
        string GetUserName(PlayerType type);
        string GetWinnerName();
    }
}