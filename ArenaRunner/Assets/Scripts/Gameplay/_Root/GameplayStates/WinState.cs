using DefaultNamespace.Gameplay.Data;
using Gameplay.Services;
using UnityEngine;
using Utils.EventBus;
using Utils.FiniteStateMachine;
using Utils.ServiceLocator;

namespace DefaultNamespace.Gameplay.GameplayStates
{
    public class WinState : FSMState<GameStateName>
    {
        readonly EventBus _eventBus;
        readonly WindowManagerService _windowManagerService;
        GameStateName _nextState;

        public WinState() : base(GameStateName.Win)
        {
            _eventBus = ServiceLocator.Current.Get<EventBus>();
            _windowManagerService = ServiceLocator.Current.Get<WindowManagerService>();
            _eventBus.OnGameEvent += EventBusOnGameEvent;
            _nextState = StateName;
        }
        public override void OnEnter()
        {
            Time.timeScale = 0;
            _windowManagerService.OpenWinPopUp();
        }

        public override void OnExit()
        {
            Time.timeScale = 1;
            _windowManagerService.ClosePopUp(WindowName.WinPopUp);
        }
        void EventBusOnGameEvent(GameEvent e)
        {
            if (e.Name == EventName.GameStateChanged)
            {
                _nextState = (GameStateName)e.Args[0];
            }
        }

        public override GameStateName GetNextState()
        {
            return _nextState;
        }
    }
}