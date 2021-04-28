using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Two.InGame.Application;
using Two.InGame.Domain.UseCase.Interface;
using Two.InGame.Presentation.View.State;
using UniRx;
using VContainer.Unity;

namespace Two.InGame.Presentation.Controller
{
    public sealed class StateSequencer : IPostInitializable
    {
        private readonly CompositeDisposable _disposable;
        private readonly CancellationTokenSource _tokenSource;

        private readonly IGameStateUseCase _gameStateUseCase;
        private readonly List<BaseState> _states;

        public StateSequencer(IGameStateUseCase gameStateUseCase, MatchingView matchingView, ReadyView readyView,
            BattleView battleView, ResultView resultView)
        {
            _disposable = new CompositeDisposable();
            _tokenSource = new CancellationTokenSource();

            _gameStateUseCase = gameStateUseCase;
            _states = new List<BaseState>
            {
                matchingView,
                readyView,
                battleView,
                resultView,
            };
        }

        ~StateSequencer()
        {
            _disposable?.Dispose();
            _tokenSource?.Cancel();
            _tokenSource?.Dispose();
        }

        public void PostInitialize()
        {
            Init();

            _gameStateUseCase.state
                .Subscribe(state =>
                {
                    Reset(state);
                    TickAsync(state, _tokenSource.Token).Forget();
                })
                .AddTo(_disposable);
        }

        private void Init()
        {
            foreach (var state in _states)
            {
                state.InitAsync(_tokenSource.Token).Forget();
            }
        }

        private void Reset(GameState gameState)
        {
            foreach (var state in _states)
            {
                state.ResetAsync(gameState, _tokenSource.Token).Forget();
            }
        }

        private async UniTaskVoid TickAsync(GameState gameState, CancellationToken token)
        {
            if (gameState == GameState.None)
            {
                return;
            }

            var currentState = _states.Find(x => x.GetState() == gameState);
            var nextState = await currentState.TickAsync(token);
            _gameStateUseCase.SetState(nextState);
        }
    }
}