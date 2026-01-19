using DefaultNamespace.Gameplay.Data;
using Gameplay.Services;
using UnityEngine;
using Utils.EventBus;
using Utils.ServiceLocator;

namespace DefaultNamespace.LosePopUp
{
    public class LosePopUpViewModel : IPopUpViewModel
    {
        readonly WindowManagerService.RemovePopUpCommand _removePopUpCommand;
        readonly EventBus _eventBus;
        public WindowName Name => WindowName.LosePopUp;
        public GameObject UIInstance { get; set; }

        public LosePopUpViewModel(WindowManagerService.RemovePopUpCommand removePopUpCommand)
        {
            _removePopUpCommand = removePopUpCommand;
            _eventBus = ServiceLocator.Current.Get<EventBus>();
        }

        public void ReloadGameplay() => _eventBus.TriggerEvent(
            new GameEvent(EventName.GameStateChanged,
                "Lose popup restart button was pressed",
                GameStateName.Init));

        public void Close()
        {
            _removePopUpCommand.Execute(this);
            Object.Destroy(UIInstance);
        }
    }
}