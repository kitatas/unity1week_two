using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;
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
    public sealed class BallView : MonoBehaviour, IBallView
    {
        private PlayerType _ownerType;
        private Transform _owner;
        private CancellationToken _token;
        private Rigidbody _rigidbody;
        private Collider _collider;

        private readonly float _shotPower = 25.0f;

        private void Awake()
        {
            _ownerType = PlayerType.None;
            _owner = null;
            _token = this.GetCancellationTokenOnDestroy();
            _rigidbody = GetComponent<Rigidbody>();
            _collider = GetComponent<Collider>();
        }

        private void Start()
        {
            this.UpdateAsObservable()
                .Where(_ => _owner != null)
                .Subscribe(_ => transform.position = _owner.position)
                .AddTo(this);
        }

        public PlayerType GetOwnerType() => _ownerType;

        public void SetOwner(Transform owner, PlayerType playerType)
        {
            _owner = owner;
            _ownerType = playerType;
            _rigidbody.velocity = Vector3.zero;
            _collider.enabled = false;
        }

        public void Shot()
        {
            _rigidbody.AddForce(_owner.forward * _shotPower, ForceMode.VelocityChange);
            _owner = null;
            GroundAsync(_token).Forget();
        }

        private async UniTaskVoid GroundAsync(CancellationToken token)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(0.1f), cancellationToken: token);

            // TODO: 壁をすり抜けてしまう場合がある
            _collider.enabled = true;

            var hitData = await this.GetAsyncCollisionEnterTrigger().OnCollisionEnterAsync(token);

            // player以外に衝突した場合
            // TODO: PlayerControllerに依存しないように修正
            if (hitData.gameObject.GetComponent<PlayerController>() == null)
            {
                _ownerType = PlayerType.None;
            }
        }
    }
}