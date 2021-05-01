using Two.Common.Application;
using Two.Common.Presentation.Controller;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Two.Common.Presentation.View
{
    [RequireComponent(typeof(Button))]
    public sealed class LoadView : MonoBehaviour
    {
        [SerializeField] private SceneName sceneName = default;

        private SceneLoader _sceneLoader;

        [Inject]
        private void Construct(SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        private void Start()
        {
            GetComponent<Button>()
                .OnClickAsObservable()
                .Subscribe(_ => _sceneLoader.Load(sceneName))
                .AddTo(this);
        }
    }
}