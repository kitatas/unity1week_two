using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;
using Photon.Pun;
using Two.InGame.Application;
using Two.InGame.Domain.UseCase.Interface;
using Two.InGame.Presentation.Controller;
using Two.InGame.Presentation.View.Interface;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using VContainer;

namespace Two.InGame.Presentation.View
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(PhotonView))]
    [RequireComponent(typeof(PhotonTransformViewClassic))]
    public sealed class BallView : MonoBehaviour, IBallView
    {
        private Vector3 _initPosition;
        private CancellationToken _token;
        private PhotonView _photonView;
        private PhotonTransformViewClassic _photonTransformViewClassic;

        private IBallMovementUseCase _ballMovementUseCase;
        private IBallColliderUseCase _ballColliderUseCase;
        private IBallOwnerUseCase _ballOwnerUseCase;

        [Inject]
        private void Construct(IBallMovementUseCase ballMovementUseCase, IBallColliderUseCase ballColliderUseCase,
            IBallOwnerUseCase ballOwnerUseCase)
        {
            _ballMovementUseCase = ballMovementUseCase;
            _ballColliderUseCase = ballColliderUseCase;
            _ballOwnerUseCase = ballOwnerUseCase;
        }

        private void Awake()
        {
            _initPosition = transform.position;
            _token = this.GetCancellationTokenOnDestroy();
            _photonView = GetComponent<PhotonView>();
            _photonTransformViewClassic = GetComponent<PhotonTransformViewClassic>();
        }

        private void Start()
        {
            var tickAsObservable = this.UpdateAsObservable()
                .Where(_ => _photonView.IsMine);

            this.UpdateAsObservable()
                .Where(_ => _ballOwnerUseCase.GetOwner() != null)
                .Subscribe(_ => transform.position = _ballOwnerUseCase.GetOwnerPosition())
                .AddTo(this);

            tickAsObservable
                .Subscribe(_ =>
                {
                    var velocity = _ballMovementUseCase.GetVelocity();
                    _photonTransformViewClassic.SetSynchronizedValues(velocity, 0);
                })
                .AddTo(this);

            // ステージ外に出た場合、初期位置に
            tickAsObservable
                .Select(_ => transform.position)
                .Where(position =>
                    position.x < -11.0f || position.x > 11.0f ||
                    position.z < -11.0f || position.z > 11.0f)
                .Subscribe(_ => transform.position = _initPosition)
                .AddTo(this);
        }

        public PlayerType GetOwnerType() => _ballOwnerUseCase.GetOwnerType();

        public void PickUp(Transform owner, PlayerType playerType)
        {
            _ballOwnerUseCase.SetOwner(owner, playerType);
            _ballMovementUseCase.ResetVelocity();
            _ballColliderUseCase.SetTrigger(true);
        }

        public void Shot()
        {
            _ballMovementUseCase.Shot(_ballOwnerUseCase.GetOwnerForward());
            _ballOwnerUseCase.ResetOwner();
            ShotAsync(_token).Forget();
        }

        private async UniTaskVoid ShotAsync(CancellationToken token)
        {
            // Playerから発射される時に当たり判定を戻す
            // Ballを2つ所持していた場合、Ball同士で判定しないように
            while (true)
            {
                var exitData = await this.GetAsyncTriggerExitTrigger().OnTriggerExitAsync(token);

                if (exitData.GetComponent<PlayerController>() != null)
                {
                    _ballColliderUseCase.SetTrigger(false);

                    break;
                }
            }

            await UniTask.DelayFrame(2, cancellationToken: token);

            var hitData = await this.GetAsyncCollisionEnterTrigger().OnCollisionEnterAsync(token);

            // player以外に衝突した場合
            // TODO: PlayerControllerに依存しないように修正
            if (hitData.gameObject.GetComponent<PlayerController>() == null)
            {
                _photonView.RPC(nameof(SyncOwnerTypeRpc), RpcTarget.All);
            }
        }

        [PunRPC]
        private void SyncOwnerTypeRpc()
        {
            _ballOwnerUseCase.ResetOwnerType();
        }
    }
}