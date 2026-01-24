using _TopDownShooter.Scripts.Gameplay.Services;
using _TopDownShooter.Scripts.Utils.EventBus;
using _TopDownShooter.Scripts.Utils.ServiceLocator;
using _TopDownShooter.Scripts.Utils.StateMachine;
using UnityEngine;

namespace _TopDownShooter.Scripts.GameplayStates
{
    public class WinState : FSMState<GameplayStateName>
    {
        readonly EventBus _eventBus;
        readonly WindowManagerService _windowManager;

        public WinState() : base(GameplayStateName.Win)
        {
            _eventBus = ServiceLocator.Current.Get<EventBus>();
            _windowManager = ServiceLocator.Current.Get<WindowManagerService>();
        }
        public override void OnEnter()
        {
            Time.timeScale = 0;
            _windowManager.OpenWinPopup();
            _eventBus.TriggerEvent(new GameEvent(EventName.GameStateChanged,
                $"Entered gameplay {StateName}",
                StateName));
        }
        public override void OnExit()
        {
            Time.timeScale = 1;
        }
        public override GameplayStateName GetNextState()
        {
            return GameplayStateName.Win;
        }
    }
}