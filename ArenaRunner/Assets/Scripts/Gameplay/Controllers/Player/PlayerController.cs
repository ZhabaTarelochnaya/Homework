using DefaultNamespace.Gameplay.Data;
using DefaultNamespace.Gameplay.World.PlayerStates;
using UnityEngine;
using Utils.EventBus;
using Utils.FiniteStateMachine;
using Utils.ServiceLocator;

namespace DefaultNamespace.Gameplay.World
{
    public class PlayerController
    {
        readonly FSM<PlayerStateName> _fsm = new ();
        public PlayerController(PlayerState playerState)
        {
            var moveService = ServiceLocator.Current.Get<MoveService>();
            _fsm.AddState(new MoveState(moveService, playerState))
                .AddState(new IdleState(playerState));
            
            var eventBus = ServiceLocator.Current.Get<EventBus>();
            playerState.PlayerDamaged += d => eventBus.TriggerEvent(
                new GameEvent(EventName.PlayerDamaged,
                $"Player was damaged, hp at: {d}",
                d));
            playerState.Died += () => eventBus.TriggerEvent(
                new GameEvent(EventName.GameStateChanged,
                    $"Player died",
                GameStateName.Lose));
        }
        public void FixedUpdate()
        {
            _fsm.Tick(Time.fixedDeltaTime);
        }
    }
}