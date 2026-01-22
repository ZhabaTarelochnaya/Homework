using System.Collections;
using _TopDownShooter.Scripts.Utils;
using _TopDownShooter.Scripts.Utils.ServiceLocator;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _TopDownShooter.Scripts.Gameplay.Services
{
    public class SceneLoaderService : IService
    {
        readonly UIRoot _uiRoot;
        readonly Coroutines _coroutines;

        public SceneLoaderService(UIRoot uiRoot, Coroutines coroutines)
        {
            _uiRoot = uiRoot;
            _coroutines = coroutines;
        }
        public void LoadGameplay() => _coroutines.StartCoroutine(LoadAndStartGameplay());

        IEnumerator LoadAndStartGameplay()
        {
            _uiRoot.ShowLoadingScreen();
            yield return LoadScene(SceneNames.Boot);
            yield return LoadScene(SceneNames.Gameplay);

            var sceneEntryPoint = Object.FindFirstObjectByType<GameplayEntryPoint>();
            sceneEntryPoint.Bind();
            _uiRoot.HideLoadingScreen();
        }
        IEnumerator LoadScene(SceneNames sceneName)
        {
            yield return SceneManager.LoadSceneAsync(sceneName.ToString());
        }
    }
}