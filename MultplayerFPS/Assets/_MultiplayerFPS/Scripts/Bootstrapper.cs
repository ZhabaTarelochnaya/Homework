using System.Collections;
using _MultiplayerFPS.Scripts.Utils;
using _MultiplayerFPS.Scripts.Utils.LoadingScreenService;
using _MultiplayerFPS.Scripts.Utils.ServiceLocator;
using UnityEngine;

public class Bootstrapper
{
    static Bootstrapper _gameRoot;
    readonly LoadingScreen _loadingScreen;
    readonly Coroutines _coroutines;
    

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void AutostartGame()
    {
        _gameRoot = new Bootstrapper(); 
        _gameRoot.RunGame();
    }

    Bootstrapper()
    {
        _coroutines = new GameObject("Coroutines").AddComponent<Coroutines>();
        Object.DontDestroyOnLoad(_coroutines.gameObject);
        
        var loadingScreenPrefab = Resources.Load<LoadingScreen>("Prefabs/LoadingScreen");
        _loadingScreen = Object.Instantiate(loadingScreenPrefab);
        Object.DontDestroyOnLoad(_loadingScreen.gameObject);
        
        ServiceLocator.Initialize();
        ServiceLocator.Current.Register<ILoadingScreenService>(_loadingScreen);
    }

    void RunGame()
    {
// #if UNITY_EDITOR
//         var sceneName = SceneManager.GetActiveScene().name;
//         if (sceneName != "Offline")
//         {
//             return;
//         }
// #endif
        //LoadScene("Offline");
    }
    public void LoadScene(string sceneName)
    {
        _coroutines.StartCoroutine(LoadSceneRoutine(sceneName));
    }
    IEnumerator LoadSceneRoutine(string sceneName)
    {
        _loadingScreen.Show();
        yield return UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);
        _loadingScreen.Hide();
    }
}
