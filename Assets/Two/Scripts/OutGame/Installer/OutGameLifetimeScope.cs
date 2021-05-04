using Two.Common.Installer;
using Two.OutGame.Domain.UseCase;
using Two.OutGame.Factory;
using UnityEngine;
using VContainer;

namespace Two.OutGame.Installer
{
    public sealed class OutGameLifetimeScope : CommonLifetimeScope
    {
        [SerializeField] private RectTransform scrollViewContent = default;
        [SerializeField] private RatingFactory ratingFactory = default;

        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);

            #region Component

            builder.RegisterInstance<RectTransform>(scrollViewContent);

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