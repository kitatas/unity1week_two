using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;
using Photon.Pun;
using Two.InGame.Application;
using Two.InGame.Presentation.Controller;
using Two.InGame.Presentation.View.Interface;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Two.InGame.Presentation.View
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(PhotonView))]
    [RequireComponent(typeof(PhotonTransformViewClassic))]
    public sealed class BallView : MonoBehaviour, IBallView
    {
        private Vector3 _initPosition;
        private PlayerType _ownerType;
        private Transform _owner;
        private CancellationToken _token;
        private Rigidbody _rigidbody;
        private Collider _collider;
        private PhotonView _photonView;
        private PhotonTransformViewClassic _photonTransformViewClassic;

        private readonly float _shotPower = 25.0f;

        private void Awake()
        {
            _initPosition = transform.position;
            _ownerType = PlayerType.None;
            _owner = null;
            _token = this.GetCancellationTokenOnDestroy();
            _rigidbody = GetComponent<Rigidbody>();
            _collider = GetComponent<Collider>();
            _photonView = GetComponent<PhotonView>();
            _photonTransformViewClassic = GetComponent<PhotonTransformViewClassic>();
        }

        private void Start()
        {
            this.UpdateAsObservable()
                .Where(_ => _owner != null)
                .Subscribe(_ => transform.position = _owner.position)
                .AddTo(this);

            this.UpdateAsObservable()
                .Where(_ => _photonView.IsMine)
                .Subscribe(_ =>
                {
                    var velocity = _rigidbody.velocity;
                    _photonTransformViewClassic.SetSynchronizedValues(velocity, 0);
                })
                .AddTo(this);

            // ステージ外に出た場合、初期位置に
            this.UpdateAsObservable()
                .Where(_ => _photonView.IsMine)
                .Select(_ => transform.position)
                .Where(position =>
                    position.x < -11.0f || position.x > 11.0f ||
                    position.z < -11.0f || position.z > 11.0f)
                .Subscribe(_ => transform.position = _initPosition)
                .AddTo(this);
        }

        public PlayerType GetOwnerType() => _ownerType;

        public void SetOwner(Transform owner, PlayerType playerType)
        {
            _owner = owner;
            _ownerType = playerType;
            _rigidbody.velocity = Vector3.zero;
            _collider.isTrigger = true;
        }

        public void Shot()
        {
            _rigidbody.AddForce(_owner.forward * _shotPower, ForceMode.VelocityChange);
            _owner = null;
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
                    _collider.isTrigger = false;
                    break;
                }
            }
            _collider.isTrigger = false;

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
            _ownerType = PlayerType.None;
        }
    }
}