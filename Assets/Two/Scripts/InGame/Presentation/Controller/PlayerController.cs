using Two.InGame.Domain.UseCase.Interface;
using Two.InGame.Presentation.View.Interface;
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
        private IRotationUseCase _rotationUseCase;
        private IBallStockUseCase _ballStockUseCase;

        [Inject]
        public void Construct(IInputProvider inputProvider, IMovementUseCase movementUseCase,
            IRotationUseCase rotationUseCase, IBallStockUseCase ballStockUseCase)
        {
            _inputProvider = inputProvider;
            _movementUseCase = movementUseCase;
            _rotationUseCase = rotationUseCase;
            _ballStockUseCase = ballStockUseCase;
        }

        private void Start()
        {
            var tickAsObservable = this.UpdateAsObservable();
            var fixedTickAsObservable = this.FixedUpdateAsObservable();

            var hitBallAsObservable = this.OnCollisionEnterAsObservable()
                .Select(other => other.gameObject.GetComponent<IBallView>())
                .Where(other => other != null);

            // 移動方向の入力
            var moveVector = Vector3.zero;
            tickAsObservable
                .Subscribe(_ => moveVector = new Vector3(_inputProvider.horizontal, 0.0f, _inputProvider.vertical))
                .AddTo(this);

            // 移動
            fixedTickAsObservable
                .Subscribe(_ => _movementUseCase.Move(moveVector.normalized))
                .AddTo(this);

            // 攻撃
            tickAsObservable
                .Where(_ => _inputProvider.isAttack)
                .Subscribe(_ => _ballStockUseCase.Shot())
                .AddTo(this);

            // 回転
            tickAsObservable
                .Subscribe(_ => _rotationUseCase.Rotate(_inputProvider.mousePosition))
                .AddTo(this);

            hitBallAsObservable
                .Subscribe(other =>
                {
                    // 取得
                    if (other.GetOwner() == null)
                    {
                        other.SetOwner(this);
                        _ballStockUseCase.PickUp(other);
                        return;
                    }

                    // ダメージ
                    if (other.GetOwner() != this)
                    {

                    }
                })
                .AddTo(this);
        }
    }
}