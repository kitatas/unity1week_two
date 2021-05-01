using Two.Common.Application;
using Two.InGame.Domain.UseCase.Interface;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Two.InGame.Presentation.View
{
    [RequireComponent(typeof(Button))]
    public sealed class TweetView : MonoBehaviour
    {
        private IMatchingUseCase _matchingUseCase;

        [Inject]
        private void Construct(IMatchingUseCase matchingUseCase)
        {
            _matchingUseCase = matchingUseCase;
        }

        private void Start()
        {
            GetComponent<Button>()
                .OnClickAsObservable()
                .Subscribe(_ =>
                {
                    var tweetText = $"{_matchingUseCase.GetTweetText()}\n";
                    tweetText += $"#{GameConfig.HASH_TAG1} #{GameConfig.HASH_TAG2} #{GameConfig.GAME_ID}\n";
                    UnityRoomTweet.Tweet(GameConfig.GAME_ID, tweetText);
                })
                .AddTo(this);
        }
    }
}