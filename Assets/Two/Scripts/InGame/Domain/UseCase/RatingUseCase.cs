using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using NCMB;
using NCMB.Extensions;
using Two.Common.Application;
using Two.Common.Domain.Repository.Interface;
using Two.InGame.Domain.UseCase.Interface;

namespace Two.InGame.Domain.UseCase
{
    public sealed class RatingUseCase : IRatingUseCase
    {
        private readonly IRankingInfoRepository _rankingInfoRepository;
        private readonly INcmbRepository _ncmbRepository;
        private readonly IPlayerDataRepository _playerDataRepository;

        public RatingUseCase(IRankingInfoRepository rankingInfoRepository, INcmbRepository ncmbRepository, IPlayerDataRepository playerDataRepository)
        {
            _rankingInfoRepository = rankingInfoRepository;
            _ncmbRepository = ncmbRepository;
            _playerDataRepository = playerDataRepository;
        }
        
        private async UniTask<NCMBObject> LoadSelfDataAsync(CancellationToken token)
        {
            var scoreData = new YieldableNcmbQuery<NCMBObject>(_rankingInfoRepository.GetClassName());
            scoreData.WhereEqualTo(Ncmb.COLUMN_OBJECT_ID, _ncmbRepository.objectId);
            await scoreData.FindAsync().ToUniTask(cancellationToken: token);

            NCMBObject ncmbObject;
            if (scoreData.count > 0)
            {
                ncmbObject = scoreData.result.First();
            }
            else
            {
                // データがない場合
                ncmbObject = new NCMBObject(_rankingInfoRepository.GetClassName());
                ncmbObject.ObjectId = _ncmbRepository.objectId;
                ncmbObject[Ncmb.COLUMN_NAME] = GameConfig.DEFAULT_NAME;
                ncmbObject[Ncmb.COLUMN_SCORE] = GameConfig.DEFAULT_RATE;
            }

            _playerDataRepository.SaveName($"{ncmbObject[Ncmb.COLUMN_NAME]}");
            var score = BuildScore($"{ncmbObject[Ncmb.COLUMN_SCORE]}");
            _playerDataRepository.SaveRate((int) score.value);
            return ncmbObject;
        }

        public async UniTask SendScoreAsync(int rate, CancellationToken token)
        {
            var ncmbObject = await LoadSelfDataAsync(token);
            ncmbObject[Ncmb.COLUMN_SCORE] = rate;
            var score = BuildScore($"{ncmbObject[Ncmb.COLUMN_SCORE]}");
            _playerDataRepository.SaveRate((int) score.value);

            NCMBException exception = null;
            await ncmbObject.YieldableSaveAsync(error => exception = error).ToUniTask(cancellationToken: token);

            if (exception != null)
            {
                // NCMBのコンソールから直接削除した場合に、該当のobjectIdが無いので発生する（らしい）
                ncmbObject.ObjectId = null;

                // 新規として送信
                await ncmbObject.YieldableSaveAsync(error => exception = error).ToUniTask(cancellationToken: token);
            }

            _ncmbRepository.objectId = ncmbObject.ObjectId;
        }
        
        private static IScore BuildScore(string scoreText)
        {
            var d = double.Parse(scoreText);
            return new NumberScore(d);
        }
    }
}