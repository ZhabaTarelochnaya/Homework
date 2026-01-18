using DefaultNamespace.Gameplay.Data;
using Utils.EventBus;
using Utils.FiniteStateMachine;
using Utils.ServiceLocator;

namespace DefaultNamespace.Gameplay.GameplayStates
{
    public class PlayingState : FSMState<GameStateName>
    {
        readonly EventBus _eventBus;
        GameStateName _nextState;
        public PlayingState() : base(GameStateName.Playing)
        {
            _eventBus = ServiceLocator.Current.Get<EventBus>();
            _eventBus.OnGameEvent += EventBusOnGameEvent;
            _nextState = StateName;
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