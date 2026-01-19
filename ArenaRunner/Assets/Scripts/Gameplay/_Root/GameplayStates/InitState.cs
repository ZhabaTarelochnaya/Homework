using DefaultNamespace.Gameplay.Data;
using Utils.EventBus;
using Utils.FiniteStateMachine;
using Utils.ServiceLocator;

namespace DefaultNamespace.Gameplay.GameplayStates
{
    public class InitState : FSMState<GameStateName>
    {
        readonly ReloadGameplayCommand _reloadGameplayCommand;
        readonly EventBus _eventBus;
        GameStateName _nextState;
        public InitState(ReloadGameplayCommand reloadGameplayCommand) : base(GameStateName.Init)
        {
            _reloadGameplayCommand = reloadGameplayCommand;
            _eventBus = ServiceLocator.Current.Get<EventBus>();
            _eventBus.OnGameEvent += EventBusOnGameEvent;
            _nextState = StateName;
        }

        public override void OnEnter()
        {
            _reloadGameplayCommand.Execute();
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