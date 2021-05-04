using System;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using NCMB;
using NCMB.Extensions;
using Two.Common.Application;
using Two.Common.Domain.Repository.Interface;
using Two.OutGame.Domain.UseCase.Interface;
using Two.OutGame.Factory;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace Two.OutGame.Domain.UseCase
{
    public sealed class RatingUseCase : IRatingUseCase
    {
        private readonly IRankingInfoRepository _rankingInfoRepository;
        private readonly INcmbRepository _ncmbRepository;
        private readonly IPlayerDataRepository _playerDataRepository;
        private readonly RectTransform _scrollViewContent;
        private readonly RatingFactory _ratingFactory;

        public RatingUseCase(IRankingInfoRepository rankingInfoRepository, INcmbRepository ncmbRepository,
            IPlayerDataRepository playerDataRepository, RectTransform scrollViewContent, RatingFactory ratingFactory)
        {
            _rankingInfoRepository = rankingInfoRepository;
            _ncmbRepository = ncmbRepository;
            _playerDataRepository = playerDataRepository;
            _scrollViewContent = scrollViewContent;
            _ratingFactory = ratingFactory;
        }

        public async UniTask LoadAllDataAsync(CancellationToken token)
        {
            DeleteAllNode(_scrollViewContent);

            // 読み込みnodeを生成
            var reading = _ratingFactory.GenerateReadingNode(_scrollViewContent);

            ActivateMask(_scrollViewContent);

            var query = new YieldableNcmbQuery<NCMBObject>(_rankingInfoRepository.GetClassName());
            query.Limit = 30;
            switch (_rankingInfoRepository.GetScoreOrder())
            {
                case ScoreOrder.OrderByAscending:
                    query.OrderByAscending(Ncmb.COLUMN_SCORE);
                    break;
                case ScoreOrder.OrderByDescending:
                    query.OrderByDescending(Ncmb.COLUMN_SCORE);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            await query.FindAsync().ToUniTask(cancellationToken: token);

            // データ取得後、読み込みnodeを削除
            Object.Destroy(reading.gameObject);

            if (query.error != null)
            {
                _ratingFactory.GenerateUnavailableNode(_scrollViewContent);
            }
            else if (query.count > 0)
            {
                int currentRank = 0;
                foreach (var ncmbObject in query.result)
                {
                    currentRank++;
                    var rankingNode = _ratingFactory.GenerateRankingNode(_scrollViewContent);
                    rankingNode.noText.text = $"{currentRank}";
                    rankingNode.nameText.text = $"{ncmbObject[Ncmb.COLUMN_NAME]}";

                    var score = BuildScore($"{ncmbObject[Ncmb.COLUMN_SCORE]}");
                    rankingNode.scoreText.text = score != null ? score.textForDisplay : Ncmb.ERROR;
                }
            }
            else
            {
                _ratingFactory.GenerateNotFoundNode(_scrollViewContent);
            }
        }

        private static void DeleteAllNode(Transform scrollViewContent)
        {
            // scroll viewにあるnodeを全削除
            var nodeCount = scrollViewContent.childCount;
            for (int i = nodeCount - 1; i >= 0; i--)
            {
                Object.Destroy(scrollViewContent.GetChild(i).gameObject);
            }
        }

        private static void ActivateMask(Transform scrollViewContent)
        {
            // 2017.2.0b3でなぜかScrollViewContentを追加しても描画されない場合がある。
            // 親maskをOFF/ONすると直るので無理やり・・・
            var mask = scrollViewContent.parent.GetComponent<Mask>();
            mask.enabled = false;
            mask.enabled = true;
        }

        public IScore BuildScore(string scoreText)
        {
            var d = double.Parse(scoreText);
            return new NumberScore(d);
        }

        public async UniTask<NCMBObject> LoadSelfDataAsync(CancellationToken token)
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

        public async UniTask SendNameAsync(string playerName, CancellationToken token)
        {
            var ncmbObject = await LoadSelfDataAsync(token);
            ncmbObject[Ncmb.COLUMN_NAME] = playerName;
            _playerDataRepository.SaveName($"{ncmbObject[Ncmb.COLUMN_NAME]}");

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
    }
}