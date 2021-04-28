using Two.InGame.Application;
using Two.InGame.Data.Entity.Interface;
using Two.InGame.Domain.Model.Interface;
using Two.InGame.Domain.UseCase.Interface;
using UniRx;

namespace Two.InGame.Domain.UseCase
{
    public sealed class MatchingStateUseCase : IMatchingStateUseCase
    {
        private readonly IMatchingStateEntity _matchingStateEntity;
        private readonly IMatchingStateModel _matchingStateModel;

        public MatchingStateUseCase(IMatchingStateEntity matchingStateEntity, IMatchingStateModel matchingStateModel)
        {
            _matchingStateEntity = matchingStateEntity;
            _matchingStateModel = matchingStateModel;
        }

        public IReadOnlyReactiveProperty<MatchingState> state => _matchingStateModel.matchingState;

        public void SetState(MatchingState value)
        {
            _matchingStateEntity.SetState(value);
            _matchingStateModel.SetState(_matchingStateEntity.GetState());
        }
    }
}