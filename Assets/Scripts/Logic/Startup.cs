using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer.Unity;

namespace MVC.Logic
{
    public class Startup : IAsyncStartable
    {
        private static int STARTUP_SCENE_ID = 0;
        private static int COMMON_SCENE_ID = 1;
        private static int LOGIC_SCENE_ID = 2;
        private static int VIEW_SCENE_ID = 3;
        private static int ENVIRONMENT_SCENE_ID = 4;

        public async UniTask StartAsync(CancellationToken cancellation = default)
        {
            await SceneManager.LoadSceneAsync(COMMON_SCENE_ID, LoadSceneMode.Additive).ToUniTask();
            await SceneManager.LoadSceneAsync(LOGIC_SCENE_ID, LoadSceneMode.Additive).ToUniTask();
            SceneManager.SetActiveScene(SceneManager.GetSceneAt(LOGIC_SCENE_ID));
            await SceneManager.LoadSceneAsync(VIEW_SCENE_ID, LoadSceneMode.Additive).ToUniTask();
            await SceneManager.LoadSceneAsync(ENVIRONMENT_SCENE_ID, LoadSceneMode.Additive).ToUniTask();
            await SceneManager.UnloadSceneAsync(STARTUP_SCENE_ID).ToUniTask();
        }
    }
}