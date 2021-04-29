using Two.Common.Application;
using UnityEngine.SceneManagement;

namespace Two.Common.Presentation.Controller
{
    public sealed class SceneLoader
    {
        public void Load(SceneName sceneName)
        {
            SceneManager.LoadScene(sceneName.ToString());
        }
    }
}