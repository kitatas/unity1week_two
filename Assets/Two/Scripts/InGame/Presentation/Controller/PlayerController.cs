using Photon.Pun;
using Two.InGame.Application;
using Two.InGame.Domain.UseCase.Interface;
using Two.InGame.Presentation.View;
using Two.InGame.Presentation.View.Interface;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using VContainer;

namespace Two.InGame.Presentation.Controller
{
    [RequireComponent(typeof(PhotonView))]
    public sealed class PlayerController : MonoBehaviour
    {
        [SerializeField] private PhotonView photonView = default;
        [SerializeField] private NameView nameView = default;
        private PlayerType _playerType;
        private PlayerType _enemyType;

        private IInputProvider _inputProvider;
        private IMovementUseCase _movementUseCase;
        private IRotationUseCase _rotationUseCase;
        private IBallStockUseCase _ballStockUseCase;
        private IHpUseCase _hpUseCase;
        private IGameStateUseCase _gameStateUseCase;
        private IMatchingUseCase _matchingUseCase;

        [Inject]
        public void Construct(IInputProvider inputProvider, IMovementUseCase movementUseCase,
            IRotationUseCase rotationUseCase, IBallStockUseCase ballStockUseCase, IHpUseCase hpUseCase,
            IGameStateUseCase gameStateUseCase, IMatchingUseCase matchingUseCase)
        {
            _inputProvider = inputProvider;
            _movementUseCase = movementUseCase;
            _rotationUseCase = rotationUseCase;
            _ballStockUseCase = ballStockUseCase;
            _hpUseCase = hpUseCase;
            _gameStateUseCase = gameStateUseCase;
            _matchingUseCase = matchingUseCase;
        }

        private void Start()
        {
            var tickAsObservable = this.UpdateAsObservable()
                .Where(_ => photonView.IsMine)
                .Where(_ => _gameStateUseCase.IsEqual(GameState.Battle));

            var fixedTickAsObservable = this.FixedUpdateAsObservable()
                .Where(_ => photonView.IsMine)
                .Where(_ => _gameStateUseCase.IsEqual(GameState.Battle));

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
                    var ballOwner = other.GetOwnerType();
                    // ダメージ
                    if (ballOwner != PlayerType.None && ballOwner != _playerType)
                    {
                        _hpUseCase.Damage();
                        if (_hpUseCase.IsDead())
                        {
                            // TODO: ゲーム終了
                        }
                    }

                    // 取得
                    other.SetOwner(transform, _playerType);
                    _ballStockUseCase.PickUp(other);
                })
                .AddTo(this);
        }

        public void SetName()
        {
            if (photonView.IsMine)
            {
                nameView.Init(_matchingUseCase.GetPlayerName());
                _playerType = _matchingUseCase.GetPlayerType();
                _enemyType = _matchingUseCase.GetEnemyType();
            }
            else
            {
                nameView.Init(_matchingUseCase.GetEnemyName());
                _playerType = _matchingUseCase.GetEnemyType();
                _enemyType = _matchingUseCase.GetPlayerType();
            }
        }
    }
}