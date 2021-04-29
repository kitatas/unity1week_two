using Two.Common.Application;
using Two.Common.Presentation.Controller;
using Two.InGame.Domain.UseCase.Interface;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Two.InGame.Presentation.View
{
    [RequireComponent(typeof(Button))]
    public sealed class DisconnectView : MonoBehaviour
    {
        private IConnectUseCase _connectUseCase;
        private SceneLoader _sceneLoader;

        [Inject]
        private void Construct(IConnectUseCase connectUseCase, SceneLoader sceneLoader)
        {
            _connectUseCase = connectUseCase;
            _sceneLoader = sceneLoader;
        }

        private void Start()
        {
            GetComponent<Button>()
                .OnClickAsObservable()
                .Subscribe(_ =>
                {
                    _connectUseCase.Disconnect();
                    _sceneLoader.Load(SceneName.Title);
                })
                .AddTo(this);
        }
    }
}