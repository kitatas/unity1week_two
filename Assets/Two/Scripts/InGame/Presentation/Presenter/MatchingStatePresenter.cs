using Two.InGame.Domain.Model.Interface;
using Two.InGame.Presentation.View;
using UniRx;
using VContainer.Unity;

namespace Two.InGame.Presentation.Presenter
{
    public sealed class MatchingStatePresenter : IPostInitializable
    {
        private readonly IMatchingStateModel _matchingStateModel;
        private readonly MatchingStateView _matchingStateView;

        public MatchingStatePresenter(IMatchingStateModel matchingStateModel, MatchingStateView matchingStateView)
        {
            _matchingStateModel = matchingStateModel;
            _matchingStateView = matchingStateView;
        }

        public void PostInitialize()
        {
            _matchingStateModel.matchingState
                .Subscribe(_matchingStateView.Show)
                .AddTo(_matchingStateView);
        }
    }
}