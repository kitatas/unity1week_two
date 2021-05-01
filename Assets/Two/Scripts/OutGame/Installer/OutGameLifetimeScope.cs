using Two.Common.Installer;
using Two.OutGame.Domain.UseCase;
using Two.OutGame.Domain.UseCase.Interface;
using VContainer;

namespace Two.OutGame.Installer
{
    public sealed class OutGameLifetimeScope : CommonLifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);

            #region UseCase

            builder.Register<INameRegisterUseCase, NameRegisterUseCase>(Lifetime.Singleton);

            #endregion
        }
    }
}