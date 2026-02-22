using System.Collections;
using _MultiplayerFPS.Scripts.Utils;
using _MultiplayerFPS.Scripts.Utils.ExceptionPopUp;
using _MultiplayerFPS.Scripts.Utils.LoadingScreenService;
using _MultiplayerFPS.Scripts.Utils.ServiceLocator;
using UnityEngine;

public class Bootstrapper
{
    static Bootstrapper _gameRoot;
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
        var loadingScreen = Object.Instantiate(loadingScreenPrefab);
        Object.DontDestroyOnLoad(loadingScreen.gameObject);
        
        var exceptionPopupViewPrefab = Resources.Load<ExceptionPopupView>("Prefabs/ExceptionPopupView");
        var exceptionPopupView = Object.Instantiate(exceptionPopupViewPrefab);
        Object.DontDestroyOnLoad(exceptionPopupView.gameObject);
        
        ServiceLocator.Initialize();
        var loadingScreenService = new LoadingScreenService(loadingScreen);
        ServiceLocator.Current.Register<ILoadingScreenService>(loadingScreenService);
        var exceptionUIService = new ExceptionUIService(exceptionPopupView);
        ServiceLocator.Current.Register<IExceptionUIService>(exceptionUIService);
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
        // }
        // public void LoadScene(string sceneName)
        // {
        //     _coroutines.StartCoroutine(LoadSceneRoutine(sceneName));
        // }
        // IEnumerator LoadSceneRoutine(string sceneName)
        // {
        //     _loadingScreen.Show();
        //     yield return UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);
        //     _loadingScreen.Hide();
        // }
    }
}
