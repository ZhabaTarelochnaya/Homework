using DefaultNamespace.Gameplay.Data;
using UnityEngine;
using Utils.FiniteStateMachine;

namespace DefaultNamespace.Gameplay.World.PlayerStates
{
    public class IdleState : FSMState<PlayerStateName>
    {
        readonly PlayerState _playerState;
        public IdleState(PlayerState playerState) : base(PlayerStateName.Idle)
        {
            _playerState = playerState;
        }

        public override void OnEnter()
        {
            _playerState.Velocity = Vector3.zero;
        }
        public override PlayerStateName GetNextState()
        {
            if (_playerState.InputMoveDirection != Vector3.zero)
            {
                return PlayerStateName.Move;
            }
            return PlayerStateName.Idle;
        }
    }
}