using Two.InGame.Domain.Model.Interface;
using Two.InGame.Presentation.View;
using UniRx;
using VContainer.Unity;

namespace Two.InGame.Presentation.Presenter
{
    public sealed class HpPresenter : IPostInitializable
    {
        private readonly IHpModel _hpModel;
        private readonly HpView _hpView;

        public HpPresenter(IHpModel hpModel, HpView hpView)
        {
            _hpModel = hpModel;
            _hpView = hpView;
        }

        public void PostInitialize()
        {
            _hpModel.hp
                .Subscribe(_hpView.Show)
                .AddTo(_hpView);
        }
    }
}