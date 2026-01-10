using System.Collections;
using DefaultNamespace;
using DefaultNamespace.Gameplay;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

public class GameEntryPoint
{
    static GameEntryPoint _gameRoot;
    readonly UIRoot _uiRoot;
    readonly Coroutines _coroutines;

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

        var prefabUIRoot = Resources.Load<UIRoot>("Prefabs/UIRoot");
        _uiRoot = Object.Instantiate(prefabUIRoot);
        Object.DontDestroyOnLoad(_uiRoot.gameObject);
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
        sceneEntryPoint.Bind();
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
