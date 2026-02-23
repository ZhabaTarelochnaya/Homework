using System;
using _MultiplayerFPS.Scripts.Utils.ExceptionPopUp;
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

    void OnEnable()
    {
        NetManager.singleton.GetComponent<NetDiscovery>().StopDiscovery(); 
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
        try
        {
            NetManager.singleton.StartHost();
            NetManager.singleton.GetComponent<NetDiscovery>().AdvertiseServer();
        }
        catch (Exception e)
        {
            ServiceLocator.Current.Get<IExceptionUIService>().ShowError(e.GetType().Name, e.Message);
        }
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
