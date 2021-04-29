using Two.Common.Domain.Repository;
using Two.Common.Domain.Repository.Interface;
using Two.OutGame.Domain.UseCase;
using Two.OutGame.Domain.UseCase.Interface;
using VContainer;
using VContainer.Unity;

namespace Two.OutGame.Installer
{
    public sealed class OutGameLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            #region Repository

            builder.Register<INameRepository, NameRepository>(Lifetime.Singleton);

            #endregion
            
            #region UseCase

            builder.Register<INameResistUseCase, NameResistUseCase>(Lifetime.Singleton);

            #endregion
        }
    }
}