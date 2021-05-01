using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Two.Common.Application;
using UnityEngine.SceneManagement;

namespace Two.Common.Presentation.Controller
{
    public sealed class SceneLoader
    {
        private readonly CancellationTokenSource _tokenSource;

        public SceneLoader()
        {
            _tokenSource = new CancellationTokenSource();
        }

        ~SceneLoader()
        {
            _tokenSource?.Cancel();
            _tokenSource?.Dispose();
        }

        public void Load(SceneName sceneName)
        {
            LoadAsync(sceneName, _tokenSource.Token).Forget();
        }

        private static async UniTask LoadAsync(SceneName sceneName, CancellationToken token)
        {
            // 効果音待ち
            await UniTask.Delay(TimeSpan.FromSeconds(0.25f), cancellationToken: token);

            SceneManager.LoadScene(sceneName.ToString());
        }
    }
}