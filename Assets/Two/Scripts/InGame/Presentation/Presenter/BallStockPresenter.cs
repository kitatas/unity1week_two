using Two.InGame.Domain.Model.Interface;
using Two.InGame.Presentation.View;
using UniRx;
using VContainer.Unity;

namespace Two.InGame.Presentation.Presenter
{
    public sealed class BallStockPresenter : IPostInitializable
    {
        private readonly IBallStockModel _ballStockModel;
        private readonly BallStockView _ballStockView;

        public BallStockPresenter(IBallStockModel ballStockModel, BallStockView ballStockView)
        {
            _ballStockModel = ballStockModel;
            _ballStockView = ballStockView;
        }

        public void PostInitialize()
        {
            _ballStockModel.stockCount
                .Subscribe(_ballStockView.Show)
                .AddTo(_ballStockView);
        }
    }
}