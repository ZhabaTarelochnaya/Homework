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
        RectTransform _popUps;
        IScreen _currentScreen;
        IPopup _popups;

        public WindowManagerService(UIRoot uiRoot)
        {
            _uiRoot = uiRoot;
            var configProvider = ServiceLocator.Current.Get<ConfigProviderService>();
            _config = configProvider.GetUIConfig();
        }
        public void SetContainers(RectTransform screens, RectTransform popUps)
        {
            _screens = screens;
            _popUps = popUps;
        }

        public void OpenGameplayUI()
        {
            var gameplayUI = _uiRoot.GetComponentInChildren<GameplayUI>();
            if (gameplayUI)
            {
                gameplayUI.Reset();
                return;
            }
            var instance = Object.Instantiate(_config.GameplayUI, _uiRoot.transform);
            gameplayUI = instance.GetComponent<GameplayUI>();
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