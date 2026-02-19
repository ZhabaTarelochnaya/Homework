using System.Collections;
using _MultiplayerFPS.Scripts.Utils;
using Mirror;

namespace _MultiplayerFPS.Scripts.Services.SceneManager
{
    public class SceneManagerService : ISceneManagerService
    {
        readonly INetworkService networkService;
        readonly LoadingScreen _loadingScreen;
        readonly Coroutines _coroutines;

        public SceneManagerService(INetworkService networkService, 
            LoadingScreen loadingScreen, Coroutines coroutines)
        {
            this.networkService = networkService;
            _loadingScreen = loadingScreen;
            _coroutines = coroutines;
        }
        public void LoadScene(string sceneName)
        {
            _coroutines.StartCoroutine(LoadSceneRoutine(sceneName));
        }

        public void LoadServerScene(string sceneName)
        {
            networkService.NetManager.ServerChangeScene(sceneName);
        }
        public void LoadLobbyAndHost()
        {
            networkService.NetManager.StartHost();
            networkService.NetDiscovery.AdvertiseServer();   
        }
        IEnumerator LoadSceneRoutine(string sceneName)
        {
            _loadingScreen.Show();
            yield return UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);
            _loadingScreen.Hide();
        }
    }
}