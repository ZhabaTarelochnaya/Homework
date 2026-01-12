using DefaultNamespace.Gameplay.Data;
using UnityEngine;
using Utils.FiniteStateMachine;

namespace DefaultNamespace.Gameplay.World.PlayerStates
{
    public class IdleState : FSMState<PlayerStateName>
    {
        readonly PlayerDataProxy _playerDataProxy;
        public IdleState(PlayerDataProxy playerDataProxy) : base(PlayerStateName.Idle)
        {
            _playerDataProxy = playerDataProxy;
        }

        public override void OnEnter()
        {
            _playerDataProxy.Velocity = Vector3.zero;
        }
        public override PlayerStateName GetNextState()
        {
            if (_playerDataProxy.Direction != Vector3.zero)
            {
                return PlayerStateName.Move;
            }
            return PlayerStateName.Idle;
        }
    }
}