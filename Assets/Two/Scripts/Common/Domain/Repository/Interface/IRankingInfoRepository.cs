namespace Two.Common.Domain.Repository.Interface
{
    public interface IRankingInfoRepository
    {
        string GetClassName();
        ScoreOrder GetScoreOrder();
    }
}