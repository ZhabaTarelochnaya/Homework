using System.Collections;
using _MultiplayerFPS.Scripts;
using _MultiplayerFPS.Scripts.Services;
using _MultiplayerFPS.Scripts.Services.SceneManager;
using _MultiplayerFPS.Scripts.Utils;
using _MultiplayerFPS.Scripts.Utils.EventBus;
using _MultiplayerFPS.Scripts.Utils.ServiceLocator;
using kcp2k;
using Mirror;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bootstrapper
{
    static Bootstrapper _gameRoot;
    readonly LoadingScreen _loadingScreen;
    readonly Coroutines _coroutines;
    readonly ISceneManagerService _sceneManagerService;
    

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
        
        var netManagerPrefab = Resources.Load<NetManager>("Prefabs/NetManager");
        var netManager = Object.Instantiate(netManagerPrefab);
        netManager.Init(_loadingScreen);
        
        ServiceLocator.Initialize();
        var networkManagerService = new NetworkService(netManager);
        ServiceLocator.Current.Register<INetworkService>(networkManagerService);
        _sceneManagerService = new SceneManagerService(networkManagerService, _loadingScreen, _coroutines);
        ServiceLocator.Current.Register(_sceneManagerService);
        var eventBus = new EventBus();
        ServiceLocator.Current.Register(eventBus);
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
        _sceneManagerService.LoadScene("Offline");
    }
}
