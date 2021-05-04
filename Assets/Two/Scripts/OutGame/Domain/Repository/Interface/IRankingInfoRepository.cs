namespace Two.OutGame.Domain.Repository.Interface
{
    public interface IRankingInfoRepository
    {
        string GetClassName();
        ScoreOrder GetScoreOrder();
    }
}