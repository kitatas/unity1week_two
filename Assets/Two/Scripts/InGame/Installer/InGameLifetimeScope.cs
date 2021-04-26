using Two.InGame.Data.Entity;
using Two.InGame.Data.Entity.Interface;
using Two.InGame.Domain.UseCase;
using Two.InGame.Domain.UseCase.Interface;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Two.InGame.Installer
{
    public sealed class InGameLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            #region Entity

            builder.Register<IBallStockEntity, BallStockEntity>(Lifetime.Scoped);

            #endregion

            #region UseCase

            builder.Register<IInputProvider, KeyboardInputProvider>(Lifetime.Scoped);
            builder.Register<IMovementUseCase, MovementUseCase>(Lifetime.Scoped);
            builder.Register<IBallStockUseCase, BallStockUseCase>(Lifetime.Scoped);

            builder.RegisterComponentInHierarchy<Rigidbody>();

            #endregion
        }
    }
}