using Two.Common.Installer;
using Two.OutGame.Domain.Repository;
using Two.OutGame.Domain.UseCase;
using Two.OutGame.Factory;
using UnityEngine;
using VContainer;

namespace Two.OutGame.Installer
{
    public sealed class OutGameLifetimeScope : CommonLifetimeScope
    {
        [SerializeField] private RectTransform scrollViewContent = default;
        [SerializeField] private RankingInfo rankingInfo = default;
        [SerializeField] private RatingFactory ratingFactory = default;

        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);

            #region Component

            builder.RegisterInstance<RectTransform>(scrollViewContent);

            #endregion

            #region DataStore

            builder.RegisterInstance<RankingInfo>(rankingInfo);

            #endregion

            #region Repository

            builder.Register<RankingInfoRepository>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<NcmbRepository>(Lifetime.Singleton).AsImplementedInterfaces();

            #endregion

            #region UseCase

            builder.Register<NameRegisterUseCase>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<RatingUseCase>(Lifetime.Singleton).AsImplementedInterfaces();

            #endregion

            #region Factory

            builder.RegisterInstance<RatingFactory>(ratingFactory);

            #endregion
        }
    }
}