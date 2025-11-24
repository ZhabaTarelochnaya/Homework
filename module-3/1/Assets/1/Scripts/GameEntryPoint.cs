using System.Collections;
using _1.Gameplay;
using _1.MainMenu;
using _1.Utils;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEntryPoint
{
    static GameEntryPoint _gameRoot;
    Coroutines _coroutines;
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void AutostartGame()
    {
        _gameRoot = new GameEntryPoint();
        _gameRoot.RunGame();
    }
    GameEntryPoint()
    {
        _coroutines = new GameObject("Coroutines").AddComponent<Coroutines>();
        Object.DontDestroyOnLoad(_coroutines.gameObject);
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
        yield return LoadScene(SceneNames.Boot);
        yield return LoadScene(SceneNames.MainMenu);
        var sceneEntryPoint = Object.FindFirstObjectByType<MainMenuEntryPoint>();
        sceneEntryPoint.Bind();
    }

    IEnumerator LoadAndStartGameplay()
    {
        yield return LoadScene(SceneNames.Boot);
        yield return LoadScene(SceneNames.Gameplay);
        var sceneEntryPoint = Object.FindFirstObjectByType<GameplayEntryPoint>();
        sceneEntryPoint.Bind();
    }

    IEnumerator LoadScene(SceneNames sceneName)
    {
        yield return SceneManager.LoadSceneAsync(sceneName.ToString());
    }
    enum SceneNames
    {
        Boot,
        Gameplay,
        MainMenu
    }
}
