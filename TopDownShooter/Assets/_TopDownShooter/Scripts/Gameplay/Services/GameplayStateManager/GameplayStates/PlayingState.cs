using _TopDownShooter.Scripts.Utils.EventBus;
using _TopDownShooter.Scripts.Utils.ServiceLocator;
using _TopDownShooter.Scripts.Utils.StateMachine;

namespace _TopDownShooter.Scripts.GameplayStates
{
    public class PlayingState : FSMState<GameplayStateName>  
    {
        readonly EventBus _eventBus;

        public PlayingState() : base(GameplayStateName.Playing)
        {
            _eventBus = ServiceLocator.Current.Get<EventBus>();
        }
        public override void OnEnter()
        {
            _eventBus.TriggerEvent(new GameEvent(EventName.GameStateChanged,
                $"Entered gameplay {StateName}",
                StateName));
        }
        public override GameplayStateName GetNextState()
        {
            return GameplayStateName.Playing;
        }
    }
}