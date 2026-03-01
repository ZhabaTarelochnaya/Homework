using System.Collections;
using _MultiplayerFPS.Scripts.Services.Config;
using _MultiplayerFPS.Scripts.Services.CoroutineRunner;
using _MultiplayerFPS.Scripts.Services.LoggerService;
using _MultiplayerFPS.Scripts.Utils.ExceptionPopUp;
using _MultiplayerFPS.Scripts.Utils.LoadingScreen;
using _MultiplayerFPS.Scripts.Utils.ServiceLocator;
using UnityEngine;
using UnityEngine.SceneManagement;
    
[DefaultExecutionOrder(-1000)]
public class Bootstrapper : MonoBehaviour
{
    [SerializeField] NetManager _netManager;
    [SerializeField] DontDestroyOnLoadUIView _dontDestroyOnLoadUIView;
    [SerializeField] ConfigsSO _configs;
    public void Awake()
    {
        DontDestroyOnLoad(_dontDestroyOnLoadUIView.gameObject);
        
        ServiceLocator.Initialize();
        var loadingScreenService = new LoadingScreenService(_dontDestroyOnLoadUIView.LoadingScreenView);
        ServiceLocator.Current.Register<ILoadingScreenService>(loadingScreenService);
        var exceptionUIService = new ExceptionUIService(_dontDestroyOnLoadUIView.ExceptionPopupView);
        ServiceLocator.Current.Register<IExceptionUIService>(exceptionUIService);
        var configsService = new ConfigService(_configs);
        ServiceLocator.Current.Register<IConfigService>(configsService);
        var consoleLoggerService = new ConsoleLoggerService();
        ServiceLocator.Current.Register<ILoggerService>(consoleLoggerService);
        var coroutineRunnerService = new CoroutineRunnerService();
        ServiceLocator.Current.Register<ICoroutineRunnerService>(coroutineRunnerService);

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
