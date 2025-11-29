using System.Collections;
using _1.Gameplay;
using _1.MainMenu;
using _1.Utils;
using TestPlatformer.Scripts;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEntryPoint
{
    static GameEntryPoint _gameRoot;
    Coroutines _coroutines;
    UIRoot _uiRoot;
    InputActions _inputActions = new ();
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
    public static void AutostartGame()
    {
        _gameRoot = new GameEntryPoint();
        _gameRoot.RunGame();
    }
    GameEntryPoint()
    {
        _coroutines = new GameObject("Coroutines").AddComponent<Coroutines>();
        Object.DontDestroyOnLoad(_coroutines.gameObject);
        
        var uIRootPrefab = Resources.Load<UIRoot>("Prefabs/UIRoot");
        _uiRoot = Object.Instantiate(uIRootPrefab);
        Object.DontDestroyOnLoad(_uiRoot.gameObject);

        
        
    }
    void RunGame()
    {
#if UNITY_EDITOR
        // var sceneName = SceneManager.GetActiveScene().name;
        var sceneName = EditorPrefs.GetString("OpennedScene");
        if (sceneName == nameof(SceneNames.Gameplay))
        {
            _coroutines.StartCoroutine(LoadAndStartGameplay());
            return;
        }
        if (sceneName == nameof(SceneNames.MainMenu))
        {
            _coroutines.StartCoroutine(LoadAndStartMainMenu());
            return;
        }
        if (sceneName != nameof(SceneNames.Boot))
        {
            return;
        }
#endif
        _coroutines.StartCoroutine(LoadAndStartMainMenu());
    }
    
    IEnumerator LoadAndStartMainMenu()
    {
        _uiRoot.ShowLoadingScreen();
        
        yield return LoadScene(SceneNames.Boot);
        yield return LoadScene(SceneNames.MainMenu);
        
        var mainMenurequests = new MainMenuRequests(Exit,
            () => _coroutines.StartCoroutine(LoadAndStartGameplay()));
        
        var sceneEntryPoint = Object.FindFirstObjectByType<MainMenuEntryPoint>();
        sceneEntryPoint.Bind(mainMenurequests, _uiRoot, _inputActions);
        
        _uiRoot.HideLoadingScreen();
    }

    IEnumerator LoadAndStartGameplay()
    {
        _uiRoot.ShowLoadingScreen();
        
        yield return LoadScene(SceneNames.Boot);
        yield return LoadScene(SceneNames.Gameplay);
        
        var gameplayRequests = new GameplayRequests(() => 
            _coroutines.StartCoroutine(LoadAndStartMainMenu()), 
            () => _coroutines.StartCoroutine(LoadAndStartGameplay()));
        
        var sceneEntryPoint = Object.FindFirstObjectByType<GameplayEntryPoint>();
        sceneEntryPoint.Bind(gameplayRequests, _uiRoot, _inputActions);
        
        _uiRoot.HideLoadingScreen();
    }

    IEnumerator LoadScene(SceneNames sceneName)
    {
        yield return SceneManager.LoadSceneAsync(sceneName.ToString());
    }

    void Exit()
    {
        Application.Quit();
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
    }
    enum SceneNames
    {
        Boot,
        Gameplay,
        MainMenu
    }
}
