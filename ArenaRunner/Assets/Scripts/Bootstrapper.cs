using System.Collections;
using DefaultNamespace;
using DefaultNamespace.Gameplay;
using DefaultNamespace.Gameplay.Data;
using DefaultNamespace.Gameplay.World;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;
using Utils.ServiceLocator;
using EventBus = Utils.EventBus.EventBus;

public class Bootstrapper
{
    static Bootstrapper _gameRoot;
    readonly UIRoot _uiRoot;
    readonly Coroutines _coroutines;
    readonly GameConfig _gameConfig;
    readonly GameState _gameState;

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

        var prefabUIRoot = Resources.Load<UIRoot>("Prefabs/UIRoot");
        _uiRoot = Object.Instantiate(prefabUIRoot);
        Object.DontDestroyOnLoad(_uiRoot.gameObject);
        
        _gameConfig = Resources.Load<GameConfig>("Configs/GameConfig");

        var gameData = new GameData();
        gameData.PlayerData = new PlayerData();
        _gameState = new GameState(gameData, _gameConfig);
        
        ServiceLocator.Initialize();

        var eventBus = new EventBus();
        ServiceLocator.Current.Register(eventBus);
        
        var gameStateService = new GameStateService(_gameState, eventBus);
        ServiceLocator.Current.Register(gameStateService);
        
        var idService = new IDService(_gameState);
        ServiceLocator.Current.Register(idService);
    }

    void RunGame()
    {
#if UNITY_EDITOR
        var sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == nameof(SceneNames.Gameplay))
        {
            _coroutines.StartCoroutine(LoadAndStartGameplay());
            return;
        }

        if (sceneName != nameof(SceneNames.Boot))
        {
            return;
        }
#endif
        _coroutines.StartCoroutine(LoadAndStartGameplay());
    }

    IEnumerator LoadAndStartGameplay()
    {
        _uiRoot.ShowLoadingScreen();
        yield return LoadScene(SceneNames.Boot);
        yield return LoadScene(SceneNames.Gameplay);

        var sceneEntryPoint = Object.FindFirstObjectByType<GameplayEntryPoint>();
        sceneEntryPoint.Bind(_gameConfig, _gameState);
        _uiRoot.HideLoadingScreen();
    }

    IEnumerator LoadScene(SceneNames sceneName)
    {
        yield return SceneManager.LoadSceneAsync(sceneName.ToString());
    }

    enum SceneNames
    {
        Boot,
        Gameplay,
    }
}
