using System;
using _MultiplayerFPS.Scripts.Utils;
using _MultiplayerFPS.Scripts.Utils.ExceptionPopUp;
using _MultiplayerFPS.Scripts.Utils.ServiceLocator;
using UnityEditor;
using UnityEngine;

namespace _MultiplayerFPS.Scripts.Offline.View.MainMenuView
{
    public class MainMenuPresenter : IPresenter
    {
        readonly IMainMenuView _mainMenuView;
        
        public event Action DiscoverClicked;

        public MainMenuPresenter(IMainMenuView mainMenuView)
        {
            _mainMenuView = mainMenuView;

            Enable();
        }
        public void Enable()
        {
            _mainMenuView.ExitButtonClicked += MainMenuViewOnExitButtonClicked;
            _mainMenuView.ClientButtonClicked += MainMenuViewOnClientButtonClicked;
            _mainMenuView.DiscoverButtonClicked += MainMenuViewOnDiscoverButtonClicked;
            _mainMenuView.HostButtonClicked += MainMenuViewOnHostButtonClicked; 
            _mainMenuView.IpInputFieldEndEdit += MainMenuViewOnIpInputFieldEndEdit;
            
            _mainMenuView.Enable();
        }
        public void Disable()
        {
            _mainMenuView.Disable();
            
            _mainMenuView.ExitButtonClicked -= MainMenuViewOnExitButtonClicked;
            _mainMenuView.ClientButtonClicked -= MainMenuViewOnClientButtonClicked;
            _mainMenuView.DiscoverButtonClicked -= MainMenuViewOnDiscoverButtonClicked;
            _mainMenuView.HostButtonClicked -= MainMenuViewOnHostButtonClicked; 
            _mainMenuView.IpInputFieldEndEdit -= MainMenuViewOnIpInputFieldEndEdit;
        }
        void MainMenuViewOnIpInputFieldEndEdit(string newIP)
        {
            NetManager.singleton.networkAddress = newIP;
        }
        void MainMenuViewOnHostButtonClicked()
        {
            try
            {
                NetManager.singleton.StartHost();
                NetManager.singleton.GetComponent<NetDiscovery>().AdvertiseServer();
            }
            catch (Exception e)
            {
                ServiceLocator.Current.Get<IExceptionUIService>()
                    .ShowError(e.GetType().Name, e.Message);
            }
        }
        void MainMenuViewOnDiscoverButtonClicked() => DiscoverClicked?.Invoke();
        void MainMenuViewOnClientButtonClicked() => NetManager.singleton.StartClient();
        void MainMenuViewOnExitButtonClicked()
        {
            #if UNITY_EDITOR
            EditorApplication.isPlaying = false;
            #endif
            Application.Quit();
        }
    }
}