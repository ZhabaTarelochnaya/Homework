using System;
using _MultiplayerFPS.Scripts.Utils.LoadingScreenService;
using _MultiplayerFPS.Scripts.Utils.ServiceLocator;
using UnityEditor;
using UnityEngine;

public class MainMenuView : MonoBehaviour
{
    [SerializeField] GameObject _selectLobbyPanel;

    void Awake()
    {
        ServiceLocator.Current.Get<ILoadingScreenService>().Hide();
    }

    public void OnExitButtonClicked()
    {
        #if UNITY_EDITOR
        EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }
    public void OnDiscoverButtonClicked()
    {
        _selectLobbyPanel.SetActive(true);
        gameObject.SetActive(false);
        NetManager.singleton.GetComponent<NetDiscovery>().StartDiscovery(); 
    }

    public void OnHostButtonClicked()
    {
        NetManager.singleton.StartHost();
        NetManager.singleton.GetComponent<NetDiscovery>().AdvertiseServer();  
    }

    public void OnClientButtonClicked()
    {
        NetManager.singleton.StartClient();
    }

    public void OnIPInputFieldEndEdit(string newIP)
    {
        NetManager.singleton.networkAddress = newIP;
    }
}
