using System.Collections;
using _MultiplayerFPS.Scripts.Utils;
using _MultiplayerFPS.Scripts.Utils.ExceptionPopUp;
using _MultiplayerFPS.Scripts.Utils.LoadingScreenService;
using _MultiplayerFPS.Scripts.Utils.ServiceLocator;
using UnityEngine;
using UnityEngine.SceneManagement;
    
[DefaultExecutionOrder(-1000)]
public class Bootstrapper : MonoBehaviour
{
    readonly Coroutines _coroutines;
    [SerializeField] NetManager _netManager;
    [SerializeField] DontDestroyOnLoadUIView _dontDestroyOnLoadUIView;
    public void Awake()
    {
        DontDestroyOnLoad(_dontDestroyOnLoadUIView.gameObject);
        
        ServiceLocator.Initialize();
        var loadingScreenService = new LoadingScreenService(_dontDestroyOnLoadUIView.LoadingScreen);
        ServiceLocator.Current.Register<ILoadingScreenService>(loadingScreenService);
        var exceptionUIService = new ExceptionUIService(_dontDestroyOnLoadUIView.ExceptionPopupView);
        ServiceLocator.Current.Register<IExceptionUIService>(exceptionUIService);

        _dontDestroyOnLoadUIView.Init(_netManager);

        StartCoroutine(LoadMainMenu());
    }
    IEnumerator LoadMainMenu()
    {
        ServiceLocator.Current.Get<ILoadingScreenService>().Show();
        yield return SceneManager.LoadSceneAsync("MainMenu");
        ServiceLocator.Current.Get<ILoadingScreenService>().Hide();
    }
    
}
