using Two.OutGame.Domain.Repository.Interface;

namespace Two.OutGame.Domain.Repository
{
    public sealed class RankingInfoRepository : IRankingInfoRepository
    {
        private readonly RankingInfo _rankingInfo;

        public RankingInfoRepository(RankingInfo rankingInfo)
        {
            _rankingInfo = rankingInfo;
        }

        public string GetClassName() => _rankingInfo.className;

        public ScoreOrder GetScoreOrder() => _rankingInfo.order;
    }
}