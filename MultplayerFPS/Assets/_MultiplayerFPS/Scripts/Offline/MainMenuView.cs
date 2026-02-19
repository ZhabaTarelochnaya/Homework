using _MultiplayerFPS.Scripts.Services;
using _MultiplayerFPS.Scripts.Services.SceneManager;
using _MultiplayerFPS.Scripts.Utils.ServiceLocator;
using UnityEditor;
using UnityEngine;

public class MainMenuView : MonoBehaviour
{
    INetworkService _networkService;
    ISceneManagerService _sceneManagerService;
    [SerializeField] GameObject _selectLobbyPanel;

    void Awake()
    {
        _networkService = ServiceLocator.Current.Get<INetworkService>();
        _sceneManagerService = ServiceLocator.Current.Get<ISceneManagerService>();
        
    }

    public void OnExitButtonClicked()
    {
        #if UNITY_EDITOR
        EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }
    public void OnConnectButtonClicked()
    {
        _selectLobbyPanel.SetActive(true);
        gameObject.SetActive(false);
        _networkService.NetDiscovery.StartDiscovery();
    }

    public void OnCreateLobbyButtonClicked()
    {
        _sceneManagerService.LoadLobbyAndHost();
    }
}
