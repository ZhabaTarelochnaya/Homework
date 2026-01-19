using DefaultNamespace.Gameplay.Data;
using Gameplay.Services;
using UnityEngine;
using Utils.EventBus;
using Utils.FiniteStateMachine;
using Utils.ServiceLocator;

namespace DefaultNamespace.Gameplay.GameplayStates
{
    public class LoseState : FSMState<GameStateName>
    {
        readonly EventBus _eventBus;
        readonly WindowManagerService _windowManagerService;
        GameStateName _nextState;

        public LoseState() : base(GameStateName.Lose)
        {
            _eventBus = ServiceLocator.Current.Get<EventBus>();
            _windowManagerService = ServiceLocator.Current.Get<WindowManagerService>();
            _eventBus.OnGameEvent += EventBusOnGameEvent;
            _nextState = StateName;
        }

        public override void OnEnter()
        {
            Time.timeScale = 0;
            _windowManagerService.OpenLosePopUp();
        }

        public override void OnExit()
        {
            Time.timeScale = 1;
            _windowManagerService.ClosePopUp(WindowName.LosePopUp);
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