using DefaultNamespace.Gameplay.Data;
using DefaultNamespace.Gameplay.Services;
using DefaultNamespace.Gameplay.World.PlayerStates;
using UnityEngine;
using Utils.FiniteStateMachine;
using Utils.ServiceLocator;

namespace DefaultNamespace.Gameplay.World
{
    public class PlayerController
    {
        readonly FSM<PlayerStateName> _fsm = new ();
        public PlayerController(PlayerDataProxy playerDataProxy)
        {
            var moveService = ServiceLocator.Current.Get<MoveService>();
            _fsm.AddState(new MoveState(moveService, playerDataProxy))
                .AddState(new IdleState(playerDataProxy));
        }
        public void FixedUpdate()
        {
            _fsm.Tick(Time.fixedDeltaTime);
        }
    }
}