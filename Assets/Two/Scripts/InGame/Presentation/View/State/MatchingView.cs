using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Two.InGame.Application;
using Two.InGame.Domain.UseCase.Interface;
using Two.InGame.Presentation.Controller;
using UnityEngine;
using VContainer;

namespace Two.InGame.Presentation.View.State
{
    public sealed class MatchingView : BaseState
    {
        private PlayerGenerator _playerGenerator;
        private IMatchingStateUseCase _matchingStateUseCase;
        private IConnectUseCase _connectUseCase;

        [Inject]
        private void Construct(PlayerGenerator playerGenerator, IMatchingStateUseCase matchingStateUseCase,
            IConnectUseCase connectUseCase)
        {
            _playerGenerator = playerGenerator;
            _matchingStateUseCase = matchingStateUseCase;
            _connectUseCase = connectUseCase;
        }

        public override GameState GetState() => GameState.Matching;

        public override UniTask InitAsync(CancellationToken token)
        {
            return base.InitAsync(token);
        }

        public override UniTask ResetAsync(GameState state, CancellationToken token)
        {
            return base.ResetAsync(state, token);
        }

        public override async UniTask<GameState> TickAsync(CancellationToken token)
        {
            try
            {
                _matchingStateUseCase.SetState(MatchingState.Connecting);
                await _connectUseCase.JoinRoomAsync(token);

                _matchingStateUseCase.SetState(MatchingState.Matching);
                var playerType = await _connectUseCase.MatchingAsync(token);

                _playerGenerator.Generate(playerType);

                _matchingStateUseCase.SetState(MatchingState.Matched);
                await UniTask.Delay(TimeSpan.FromSeconds(2.0f), cancellationToken: token);

                _matchingStateUseCase.SetState(MatchingState.None);
                return GameState.Ready;
            }
            catch (Exception e)
            {
                Debug.LogError($"connect error: {e}");
                throw;
            }
        }

        public override UniTask DisposeAsync(CancellationToken token)
        {
            return base.DisposeAsync(token);
        }
    }
}