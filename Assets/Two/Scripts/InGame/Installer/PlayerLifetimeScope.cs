using Two.InGame.Data.Entity;
using Two.InGame.Domain.Model;
using Two.InGame.Domain.UseCase;
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
        [SerializeField] private HpView hpView = default;

        protected override void Configure(IContainerBuilder builder)
        {
            #region Component

            builder.RegisterInstance<Camera>(FindObjectOfType<Camera>());
            builder.RegisterInstance<Transform>(transform);
            builder.RegisterInstance<Rigidbody>(GetComponent<Rigidbody>());

            #endregion

            #region Entity

            builder.Register<BallStockEntity>(Lifetime.Scoped).AsImplementedInterfaces();
            builder.Register<HpEntity>(Lifetime.Scoped).AsImplementedInterfaces();

            #endregion

            #region Model

            builder.Register<BallStockModel>(Lifetime.Scoped).AsImplementedInterfaces();
            builder.Register<HpModel>(Lifetime.Scoped).AsImplementedInterfaces();

            #endregion

            #region UseCase

            builder.Register<KeyboardInputProvider>(Lifetime.Scoped).AsImplementedInterfaces();
            builder.Register<MovementUseCase>(Lifetime.Scoped).AsImplementedInterfaces();
            builder.Register<BallStockUseCase>(Lifetime.Scoped).AsImplementedInterfaces();
            builder.Register<RotationUseCase>(Lifetime.Scoped).AsImplementedInterfaces();
            builder.Register<HpUseCase>(Lifetime.Scoped).AsImplementedInterfaces();

            #endregion

            #region View

            builder.RegisterInstance<BallStockView>(ballStockView);
            builder.RegisterInstance<HpView>(hpView);

            #endregion

            #region Presenter

            builder.RegisterEntryPoint<BallStockPresenter>(Lifetime.Scoped);
            builder.RegisterEntryPoint<HpPresenter>(Lifetime.Scoped);

            #endregion
        }
    }
}