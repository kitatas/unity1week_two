using Two.InGame.Domain.UseCase.Interface;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using VContainer;

namespace Two.InGame.Presentation.Controller
{
    public sealed class PlayerController : MonoBehaviour
    {
        private IInputProvider _inputProvider;
        private IMovementUseCase _movementUseCase;
        private IBallStockUseCase _ballStockUseCase;

        [Inject]
        public void Construct(IInputProvider inputProvider, IMovementUseCase movementUseCase,
            IBallStockUseCase ballStockUseCase)
        {
            _inputProvider = inputProvider;
            _movementUseCase = movementUseCase;
            _ballStockUseCase = ballStockUseCase;
        }

        private void Start()
        {
            var moveVector = Vector3.zero;
            this.UpdateAsObservable()
                .Subscribe(_ => moveVector = new Vector3(_inputProvider.horizontal, 0.0f, _inputProvider.vertical))
                .AddTo(this);

            this.FixedUpdateAsObservable()
                .Subscribe(_ => _movementUseCase.Move(moveVector.normalized))
                .AddTo(this);

            this.UpdateAsObservable()
                .Where(_ => _inputProvider.isAttack)
                .Subscribe(_ => _ballStockUseCase.Shot())
                .AddTo(this);


            // TODO: 仮の取得
            this.UpdateAsObservable()
                .Where(_ => Input.GetKeyDown(KeyCode.Return))
                .Subscribe(_ => _ballStockUseCase.PickUp())
                .AddTo(this);
        }
    }
}