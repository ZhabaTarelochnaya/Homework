using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using _RSS.Scripts;
using _RSS.Scripts.Data;
using _RSS.Scripts.Gameplay;
using _RSS.Scripts.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

public class GameEntryPoint 
{
    static GameEntryPoint _gameRoot;
    readonly UIRoot _uiRoot;
    readonly RSSFeed _rssFeed;
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

        var rssFeed = Resources.Load<RSSFeed>("Prefabs/UI/RSSFeed");
        _rssFeed = Object.Instantiate(rssFeed, _uiRoot.transform);
        
        var cts = new CancellationTokenSource();
        LoadNews(_rssFeed, cts.Token);
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

    async void LoadNews(RSSFeed feed, CancellationToken ct)
    {
        var newsData = await NewsLoader.LoadNewsAsync("/TestNews", ct);
        var newsList = newsData.Select(n => new NewsItem(n)).ToList();
        var news = new News(newsList);
        feed.Bind(news);
    }
}
