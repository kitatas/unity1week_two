using Two.Common.Domain.Repository;
using Two.Common.Domain.Repository.Interface;
using Two.Common.Presentation.Controller;
using VContainer;
using VContainer.Unity;

namespace Two.Common.Installer
{
    public abstract class CommonLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            #region Repository

            builder.Register<INameRepository, NameRepository>(Lifetime.Singleton);

            #endregion

            #region Controller

            builder.Register<SceneLoader>(Lifetime.Singleton);

            #endregion
        }
    }
}