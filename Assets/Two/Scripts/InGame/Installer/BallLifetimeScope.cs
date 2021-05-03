using Two.InGame.Domain.UseCase;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Two.InGame.Installer
{
    public sealed class BallLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            #region Component

            builder.RegisterInstance<Rigidbody>(GetComponent<Rigidbody>());
            builder.RegisterInstance<Collider>(GetComponent<Collider>());

            #endregion

            #region UseCase

            builder.Register<BallColliderUseCase>(Lifetime.Scoped).AsImplementedInterfaces();
            builder.Register<BallMovementUseCase>(Lifetime.Scoped).AsImplementedInterfaces();
            builder.Register<BallOwnerUseCase>(Lifetime.Scoped).AsImplementedInterfaces();

            #endregion
        }
    }
}