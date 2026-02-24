using System;
using _MultiplayerFPS.Scripts.Offline.View.MainMenuView;
using _MultiplayerFPS.Scripts.Utils.LoadingScreen;
using _MultiplayerFPS.Scripts.Utils.ServiceLocator;
using UnityEngine;

namespace _MultiplayerFPS.Scripts.Offline
{
    [DefaultExecutionOrder(-100)]
    public class MainMenuRoot : MonoBehaviour
    {
        SelectLobbyPresenter _selectLobbyPresenter;
        MainMenuPresenter _mainMenuPresenter;
        [SerializeField] MainMenuView _mainMenuView;
        [SerializeField] SelectLobbyView _selectLobbyView;

        void Awake()
        {
            ServiceLocator.Current.Get<ILoadingScreenService>().Hide();
            
            _selectLobbyPresenter = new SelectLobbyPresenter(_selectLobbyView);
            _mainMenuPresenter = new MainMenuPresenter(_mainMenuView);
            
            _mainMenuPresenter.DiscoverClicked += MainMenuPresenterOnDiscoverClicked;
            _selectLobbyPresenter.ExitClicked += SelectLobbyPresenterOnExitClicked;
            
            _selectLobbyPresenter.Disable();
        }
        void MainMenuPresenterOnDiscoverClicked()
        {
            _mainMenuView.Disable();
            _selectLobbyPresenter.Enable();
        }
        void SelectLobbyPresenterOnExitClicked()
        {
            _selectLobbyPresenter.Disable();
            _mainMenuView.Enable();
        }
        void OnDestroy()
        {
            _mainMenuPresenter.DiscoverClicked -= MainMenuPresenterOnDiscoverClicked;
            _selectLobbyPresenter.ExitClicked -= SelectLobbyPresenterOnExitClicked;
        }
    }
}