
using System.Collections;
using _Prototype.Scripts.Utils;
using _Prototype.Scripts.Utils.ServiceLocator;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Prototype.Scripts.Services
{
    public class SceneLoaderService : IService
    {
        readonly LoadingScreen _loadingScreen;
        readonly Coroutines _coroutines;

        public SceneLoaderService(LoadingScreen loadingScreen, Coroutines coroutines)
        {
            _loadingScreen = loadingScreen;
            _coroutines = coroutines;
        }
        public void LoadGameplay() => _coroutines.StartCoroutine(LoadAndStartGameplay());

        IEnumerator LoadAndStartGameplay()
        {
            _loadingScreen.Show();
            yield return LoadScene("Load");
            yield return LoadScene("Gameplay");
            _loadingScreen.Hide();
        }
        IEnumerator LoadScene(string sceneName)
        {
            yield return SceneManager.LoadSceneAsync(sceneName);
        }
    }
}