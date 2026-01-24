using System.Collections.Generic;
using _TopDownShooter.Scripts.Gameplay.Configs;
using _TopDownShooter.Scripts.Utils.ServiceLocator;
using _TopDownShooter.Scripts.View;
using UnityEngine;

namespace _TopDownShooter.Scripts.Gameplay.Services
{
    public class WindowManagerService : IService
    {
        readonly UIRoot _uiRoot;
        readonly UIConfig _config;
        RectTransform _screens;
        RectTransform _popups;
        IScreen _currentScreen;
        List<IPopup> _currentPopups = new();

        public WindowManagerService(UIRoot uiRoot)
        {
            _uiRoot = uiRoot;
            var configProvider = ServiceLocator.Current.Get<ConfigProviderService>();
            _config = configProvider.GetUIConfig();
        }
        public void SetContainers(RectTransform screens, RectTransform popUps)
        {
            _screens = screens;
            _popups = popUps;
        }

        public void OpenGameplayUI()
        {
            var gameplayUI = _uiRoot.GetComponentInChildren<GameplayUI>();
            if (gameplayUI)
            {
                gameplayUI.Reset();
            }
            else
            {
                var instance = Object.Instantiate(_config.GameplayUI, _uiRoot.transform);
                gameplayUI = instance.GetComponent<GameplayUI>();
            }
            SetContainers(gameplayUI.Screens, gameplayUI.Popups);
            gameplayUI.Bind(this);
        }
        public HUD OpenHUD()
        {
            var instance = Object.Instantiate(_config.HUD, _screens);
            var hud = instance.GetComponent<HUD>();
            SetScreen(hud);
            hud.Bind();
            return hud;
        }
        public void OpenLosePopup()
        {
            var instance = Object.Instantiate(_config.LosePopup, _popups);
            var gameEndPopup = instance.GetComponent<GameEndPopup>();
            gameEndPopup.Bind();
            _currentPopups.Add(gameEndPopup);
        }
        public void OpenWinPopup()
        {
            var instance = Object.Instantiate(_config.WinPopup, _popups);
            var gameEndPopup = instance.GetComponent<GameEndPopup>();
            gameEndPopup.Bind();
            _currentPopups.Add(gameEndPopup);
        }

        public void ClearPopups()
        {
            foreach (var popup in _currentPopups)
            {
                Object.Destroy(popup.gameObject);
            }
            _currentPopups.Clear();
        }
        void SetScreen(IScreen screen)
        {
            if (_currentScreen != null)
            {
                Object.Destroy(_currentScreen.gameObject);
            }
            _currentScreen = screen;
        }
    }
}