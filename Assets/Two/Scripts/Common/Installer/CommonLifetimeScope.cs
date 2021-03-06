using Two.Common.Data.DataStore;
using Two.Common.Domain.Repository;
using Two.Common.Domain.UseCase;
using Two.Common.Presentation.Controller;
using Two.Common.Presentation.Controller.Sound;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Two.Common.Installer
{
    public abstract class CommonLifetimeScope : LifetimeScope
    {
        [SerializeField] private BgmTable bgmTable = default;
        [SerializeField] private SeTable seTable = default;
        [SerializeField] private RankingInfo rankingInfo = default;

        protected override void Configure(IContainerBuilder builder)
        {
            #region DataStore

            builder.RegisterInstance<BgmTable>(bgmTable);
            builder.RegisterInstance<SeTable>(seTable);
            builder.RegisterInstance<RankingInfo>(rankingInfo);

            #endregion

            #region Repository

            builder.Register<PlayerDataRepository>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<SoundRepository>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<RankingInfoRepository>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<NcmbRepository>(Lifetime.Singleton).AsImplementedInterfaces();

            #endregion

            #region UseCase

            builder.Register<SoundUseCase>(Lifetime.Singleton).AsImplementedInterfaces();

            #endregion

            #region Controller

            builder.Register<SceneLoader>(Lifetime.Singleton);
            builder.RegisterInstance(FindObjectOfType<BgmController>());
            builder.RegisterInstance(FindObjectOfType<SeController>());

            #endregion
        }
    }
}