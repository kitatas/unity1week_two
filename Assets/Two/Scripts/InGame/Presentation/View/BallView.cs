using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;
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
        private bool _isOwn;

        private PlayerController _owner;
        private CancellationToken _token;
        private Rigidbody _rigidbody;
        private Collider _collider;

        private readonly float _shotPower = 25.0f;

        private void Awake()
        {
            _isOwn = false;
            _owner = null;
            _token = this.GetCancellationTokenOnDestroy();
            _rigidbody = GetComponent<Rigidbody>();
            _collider = GetComponent<Collider>();
        }

        private void Start()
        {
            this.UpdateAsObservable()
                .Where(_ => GetOwner() != null && _isOwn)
                .Subscribe(_ => transform.position = GetOwner().transform.position)
                .AddTo(this);
        }

        public PlayerController GetOwner() => _owner;

        public void SetOwner(PlayerController owner)
        {
            if (owner == null)
            {
                _isOwn = false;
                _rigidbody.AddForce(GetOwner().transform.forward * _shotPower, ForceMode.VelocityChange);
                GroundAsync(_token).Forget();
            }
            else
            {
                _owner = owner;
                _isOwn = true;
                _rigidbody.velocity = Vector3.zero;
                _collider.enabled = false;
            }
        }

        private async UniTaskVoid GroundAsync(CancellationToken token)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(0.1f), cancellationToken: token);

            _collider.enabled = true;

            await this.GetAsyncCollisionEnterTrigger().OnCollisionEnterAsync(token);

            _owner = null;
        }
    }
}