using System.Collections;
using _TopDownShooter.Scripts;
using _TopDownShooter.Scripts.Gameplay.Services;
using _TopDownShooter.Scripts.Utils;
using _TopDownShooter.Scripts.Utils.EventBus;
using _TopDownShooter.Scripts.Utils.ServiceLocator;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEntryPoint
{
    static GameEntryPoint _gameRoot;
    readonly SceneLoaderService _sceneLoaderService;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void AutostartGame()
    {
        _gameRoot = new GameEntryPoint();
        _gameRoot.RunGame();
    }

    GameEntryPoint()
    {
        var coroutines = new GameObject("Coroutines").AddComponent<Coroutines>();
        Object.DontDestroyOnLoad(coroutines.gameObject);

        var prefabUIRoot = Resources.Load<UIRoot>("Prefabs/UIRoot");
        var uiRoot = Object.Instantiate(prefabUIRoot);
        Object.DontDestroyOnLoad(uiRoot.gameObject);
        
        ServiceLocator.Initialize();
        
        var eventBus = new EventBus();
        ServiceLocator.Current.Register(eventBus);
        
        var gameConfig = Resources.Load<GameConfig>("Configs/GameConfig");
        var configProviderService = new ConfigProviderService(gameConfig);
        ServiceLocator.Current.Register(configProviderService);
        
        var windowManagerService = new WindowManagerService(uiRoot);
        ServiceLocator.Current.Register(windowManagerService);
        
        _sceneLoaderService = new SceneLoaderService(uiRoot, coroutines);
        ServiceLocator.Current.Register(_sceneLoaderService);
    }

    void RunGame()
    {
#if UNITY_EDITOR
        var sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == nameof(SceneNames.Gameplay))
        {
            _sceneLoaderService.LoadGameplay();
            return;
        }

        if (sceneName != nameof(SceneNames.Boot))
        {
            return;
        }
#endif
        _sceneLoaderService.LoadGameplay();
    }
}
