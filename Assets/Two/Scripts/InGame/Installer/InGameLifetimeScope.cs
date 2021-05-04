using Two.Common.Installer;
using Two.InGame.Data.Entity;
using Two.InGame.Domain.Model;
using Two.InGame.Domain.UseCase;
using Two.InGame.Factory;
using Two.InGame.Presentation.Controller;
using Two.InGame.Presentation.Presenter;
using Two.InGame.Presentation.View;
using Two.InGame.Presentation.View.State;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Two.InGame.Installer
{
    public sealed class InGameLifetimeScope : CommonLifetimeScope
    {
        [SerializeField] private MatchingView matchingView = default;
        [SerializeField] private ReadyView readyView = default;
        [SerializeField] private BattleView battleView = default;
        [SerializeField] private ResultView resultView = default;
        [SerializeField] private MatchingStateView matchingStateView = default;
        [SerializeField] private MatchingUserView matchingUserView = default;
        [SerializeField] private PunObjectFactory punObjectFactory = default;
        [SerializeField] private LocalObjectFactory localObjectFactory = default;

        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);

            #region Entity

            builder.Register<GameStateEntity>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<MatchingEntity>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<MatchingStateEntity>(Lifetime.Singleton).AsImplementedInterfaces();

            #endregion

            #region Model

            builder.Register<GameStateModel>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<MatchingStateModel>(Lifetime.Singleton).AsImplementedInterfaces();

            #endregion

            #region UseCase

            builder.Register<ConnectUseCase>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<GameStateUseCase>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<MatchingStateUseCase>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<MatchingUseCase>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<RatingUseCase>(Lifetime.Singleton).AsImplementedInterfaces();

            #endregion

            #region View

            builder.RegisterInstance<MatchingView>(matchingView);
            builder.RegisterInstance<ReadyView>(readyView);
            builder.RegisterInstance<BattleView>(battleView);
            builder.RegisterInstance<ResultView>(resultView);
            builder.RegisterInstance<MatchingStateView>(matchingStateView);
            builder.RegisterInstance<MatchingUserView>(matchingUserView);

            #endregion

            #region Presenter

            builder.RegisterEntryPoint<MatchingStatePresenter>(Lifetime.Singleton);

            #endregion

            #region Controller

            builder.RegisterEntryPoint<StateSequencer>(Lifetime.Singleton);

            #endregion

            #region Factory

            builder.RegisterInstance<PunObjectFactory>(punObjectFactory);
            builder.RegisterInstance<LocalObjectFactory>(localObjectFactory);

            #endregion
        }
    }
}