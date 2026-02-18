using System.Collections;
using _MultiplayerFPS.Scripts.Utils;
using Mirror;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bootstrapper
{
    static Bootstrapper _gameRoot;
    readonly LoadingScreen _loadingScreen;
    readonly Coroutines _coroutines;
    readonly NetworkManager _networkManager;

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
        
        var prefabUIRoot = Resources.Load<LoadingScreen>("Prefabs/LoadingScreen");
        _loadingScreen = Object.Instantiate(prefabUIRoot);
        Object.DontDestroyOnLoad(_loadingScreen.gameObject);
        
        var networkManagerPrefab = Resources.Load<NetworkManager>("Prefabs/NetworkManager");
        _networkManager = Object.Instantiate(networkManagerPrefab);
    }

    void RunGame()
    {
#if UNITY_EDITOR
        var sceneName = SceneManager.GetActiveScene().name;
        if (sceneName != "Offline")
        {
            return;
        }
#endif
        _coroutines.StartCoroutine(LoadAndStart("Offline"));
    }

    IEnumerator LoadAndStart(string sceneName)
    {
        _loadingScreen.Show();
        yield return LoadScene(sceneName);
        _loadingScreen.Hide();
    }
    IEnumerator LoadScene(string sceneName)
    {
        yield return SceneManager.LoadSceneAsync(sceneName);
    }
}
