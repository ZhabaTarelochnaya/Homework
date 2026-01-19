using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using DefaultNamespace.Gameplay.Data;
using DefaultNamespace.LosePopUp;
using DefaultNamespace.WinPopUp;
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
        List<IPopUpViewModel> _popUps = new ();
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
            _screenViewModel?.Close();
            var instance = Object.Instantiate(_uiConfig.HUDPrefab, _screenContainer);
            var hudView = instance.GetComponent<HUDView>();
            var hudViewModel = new HUDViewModel();
            hudView.Bind(hudViewModel);
        }
        public void OpenWinPopUp()
        {
            var instance = Object.Instantiate(_uiConfig.WinPopUpPrefab, _popUpsContainer);
            var winPopUpView = instance.GetComponent<WinPopUpView>();
            var removePopUpCommand = new RemovePopUpCommand(_popUps);
            var winPopUpViewModel = new WinPopUpViewModel(removePopUpCommand);
            winPopUpView.Bind(winPopUpViewModel);
            _popUps.Add(winPopUpViewModel);
        }

        public void ClosePopUp(WindowName name)
        {
            _popUps.FirstOrDefault(p => p.Name == name)?.Close();
        }
        public void OpenLosePopUp()
        {
            var instance = Object.Instantiate(_uiConfig.LosePopUpPrefab, _popUpsContainer);
            var losePopUpView = instance.GetComponent<LosePopUpView>();
            var removePopUpCommand = new RemovePopUpCommand(_popUps);
            var losePopUpViewModel = new LosePopUpViewModel(removePopUpCommand);
            losePopUpView.Bind(losePopUpViewModel);
            _popUps.Add(losePopUpViewModel);
        }

        public void CloseAllPopUps()
        {
            while (_popUps.Count > 0)
            {
                _popUps[^1].Close();
            }
        }
        public class RemovePopUpCommand
        {
            readonly List<IPopUpViewModel> _popUps;

            public RemovePopUpCommand(List<IPopUpViewModel> popUps)
            {
                _popUps = popUps;
            }

            public void Execute(IPopUpViewModel popUp)
            {
                _popUps.Remove(popUp);
            }
        }
    }
}