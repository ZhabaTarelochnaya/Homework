using Gameplay.Services;
using UnityEngine;
using Utils.EventBus;
using Utils.ServiceLocator;

namespace DefaultNamespace
{
    public class GameplayUIViewModel
    {
        WindowManagerService _windowManagerService;
        public GameplayUIViewModel()
        {
            _windowManagerService = ServiceLocator.Current.Get<WindowManagerService>();
        }
        
        public void SetContainers(Transform screens,  Transform popUps)
        {
            _windowManagerService.SetContainers(screens, popUps);
        }

        public void OpenHUD() => _windowManagerService.OpenHUD();
    }
}