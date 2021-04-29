using Two.Common.Domain.Repository;
using Two.Common.Domain.Repository.Interface;
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
        [SerializeField] private PlayerGenerator playerGenerator = default;

        protected override void Configure(IContainerBuilder builder)
        {
            #region Entity

            builder.Register<IGameStateEntity, GameStateEntity>(Lifetime.Singleton);
            builder.Register<IMatchingEntity, MatchingEntity>(Lifetime.Singleton);
            builder.Register<IMatchingStateEntity, MatchingStateEntity>(Lifetime.Singleton);

            #endregion

            #region Model

            builder.Register<IGameStateModel, GameStateModel>(Lifetime.Singleton);
            builder.Register<IMatchingStateModel, MatchingStateModel>(Lifetime.Singleton);

            #endregion

            #region Repository

            builder.Register<INameRepository, NameRepository>(Lifetime.Singleton);

            #endregion

            #region UseCase

            builder.Register<IConnectUseCase, ConnectUseCase>(Lifetime.Singleton);
            builder.Register<IGameStateUseCase, GameStateUseCase>(Lifetime.Singleton);
            builder.Register<IMatchingStateUseCase, MatchingStateUseCase>(Lifetime.Singleton);
            builder.Register<IMatchingUseCase, MatchingUseCase>(Lifetime.Singleton);

            #endregion

            #region View

            builder.RegisterInstance<MatchingView>(matchingView);
            builder.RegisterInstance<ReadyView>(readyView);
            builder.RegisterInstance<BattleView>(battleView);
            builder.RegisterInstance<ResultView>(resultView);

            #endregion

            #region Controller

            builder.RegisterEntryPoint<StateSequencer>(Lifetime.Singleton);
            builder.RegisterInstance<PlayerGenerator>(playerGenerator);

            #endregion
        }
    }
}