using Two.Common.Data.DataStore;
using Two.Common.Domain.Repository;
using Two.Common.Domain.Repository.Interface;
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
        [SerializeField] private BgmController bgmController = default;
        [SerializeField] private SeController seController = default;

        protected override void Configure(IContainerBuilder builder)
        {
            #region DataStore

            builder.RegisterInstance<BgmTable>(bgmTable);
            builder.RegisterInstance<SeTable>(seTable);

            #endregion

            #region Repository

            builder.Register<INameRepository, NameRepository>(Lifetime.Singleton);
            builder.Register<ISoundRepository, SoundRepository>(Lifetime.Singleton);

            #endregion

            #region UseCase

            builder.Register<SoundUseCase>(Lifetime.Singleton)
                .AsImplementedInterfaces();

            #endregion

            #region Controller

            builder.Register<SceneLoader>(Lifetime.Singleton);
            builder.RegisterInstance(bgmController);
            builder.RegisterInstance(seController);

            #endregion
        }
    }
}