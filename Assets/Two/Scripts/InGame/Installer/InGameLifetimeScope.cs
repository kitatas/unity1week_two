using Two.InGame.Data.Entity;
using Two.InGame.Data.Entity.Interface;
using Two.InGame.Domain.Model;
using Two.InGame.Domain.Model.Interface;
using Two.InGame.Domain.UseCase;
using Two.InGame.Domain.UseCase.Interface;
using Two.InGame.Presentation.Controller;
using Two.InGame.Presentation.View.State;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Two.InGame.Installer
{
    public sealed class InGameLifetimeScope : LifetimeScope
    {
        [SerializeField] private MatchingView matchingView = default;
        [SerializeField] private ReadyView readyView = default;
        [SerializeField] private BattleView battleView = default;
        [SerializeField] private ResultView resultView = default;

        protected override void Configure(IContainerBuilder builder)
        {
            #region Entity

            builder.Register<IGameStateEntity, GameStateEntity>(Lifetime.Singleton);

            #endregion

            #region Model

            builder.Register<IGameStateModel, GameStateModel>(Lifetime.Singleton);

            #endregion

            #region UseCase

            builder.Register<IGameStateUseCase, GameStateUseCase>(Lifetime.Singleton);

            #endregion

            #region View

            builder.RegisterInstance<MatchingView>(matchingView);
            builder.RegisterInstance<ReadyView>(readyView);
            builder.RegisterInstance<BattleView>(battleView);
            builder.RegisterInstance<ResultView>(resultView);

            #endregion

            #region Controller

            builder.RegisterEntryPoint<StateSequencer>(Lifetime.Singleton);

            #endregion
        }
    }
}