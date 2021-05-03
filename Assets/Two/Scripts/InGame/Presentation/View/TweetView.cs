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
        private ITweetUseCase _tweetUseCase;

        [Inject]
        private void Construct(ITweetUseCase tweetUseCase)
        {
            _tweetUseCase = tweetUseCase;
        }

        private void Start()
        {
            GetComponent<Button>()
                .OnClickAsObservable()
                .Subscribe(_ => UnityRoomTweet.Tweet(GameConfig.GAME_ID, _tweetUseCase.GetTweetText()))
                .AddTo(this);
        }
    }
}