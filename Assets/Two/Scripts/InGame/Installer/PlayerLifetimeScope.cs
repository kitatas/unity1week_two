using Two.InGame.Data.Entity;
using Two.InGame.Data.Entity.Interface;
using Two.InGame.Domain.Model;
using Two.InGame.Domain.Model.Interface;
using Two.InGame.Domain.UseCase;
using Two.InGame.Domain.UseCase.Interface;
using Two.InGame.Presentation.Presenter;
using Two.InGame.Presentation.View;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Two.InGame.Installer
{
    public sealed class PlayerLifetimeScope : LifetimeScope
    {
        [SerializeField] private BallStockView ballStockView = default;

        protected override void Configure(IContainerBuilder builder)
        {
            #region Component

            builder.RegisterInstance<Camera>(FindObjectOfType<Camera>());
            builder.RegisterInstance<Transform>(transform);
            builder.RegisterInstance<Rigidbody>(GetComponent<Rigidbody>());

            #endregion

            #region Entity

            builder.Register<IBallStockEntity, BallStockEntity>(Lifetime.Scoped);

            #endregion

            #region Model

            builder.Register<IBallStockModel, BallStockModel>(Lifetime.Scoped);

            #endregion

            #region UseCase

            builder.Register<IInputProvider, KeyboardInputProvider>(Lifetime.Scoped);
            builder.Register<IMovementUseCase, MovementUseCase>(Lifetime.Scoped);
            builder.Register<IBallStockUseCase, BallStockUseCase>(Lifetime.Scoped);
            builder.Register<IRotationUseCase, RotationUseCase>(Lifetime.Scoped);

            #endregion

            #region View

            builder.RegisterInstance<BallStockView>(ballStockView);

            #endregion

            #region Presenter

            builder.RegisterEntryPoint<BallStockPresenter>(Lifetime.Scoped);

            #endregion
        }
    }
}