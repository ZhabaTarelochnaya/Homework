using System.Collections.Generic;
using DefaultNamespace;
using DefaultNamespace.Gameplay.Data;
using UnityEngine;
using Utils.ServiceLocator;

namespace Gameplay.Services
{
    public class WindowManagerService : IService
    {
        Transform _screenContainer;
        Transform _popUpsContainer;
        UIConfig _uiConfig;
        IScreenViewModel _screenViewModel;
        List<IPopUpViewModel> _popUps;
        public WindowManagerService(UIConfig uiConfig)
        {
            _uiConfig = uiConfig;
        }

        public void SetContainers(Transform screenContainer, Transform popUpsContainer)
        {
            _screenContainer = screenContainer;
            _popUpsContainer = popUpsContainer;
        }

        public void OpenHUD()
        {
            var instance = Object.Instantiate(_uiConfig.HUDPrefab, _screenContainer);
            var hudView = instance.GetComponent<HUDView>();
            var hudViewModel = new HUDViewModel();
            hudView.Bind(hudViewModel);
        }
    }
}