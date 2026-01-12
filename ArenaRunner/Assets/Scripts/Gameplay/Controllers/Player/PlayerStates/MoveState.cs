using DefaultNamespace.Gameplay.Data;
using UnityEngine;
using Utils.FiniteStateMachine;

namespace DefaultNamespace.Gameplay.World.PlayerStates
{
    public class MoveState : FSMState<PlayerStateName>
    {
        readonly MoveService _moveService;
        readonly PlayerDataProxy _playerDataProxy;

        public MoveState(MoveService moveService, PlayerDataProxy playerDataProxy) 
            : base(PlayerStateName.Move)
        {
            _moveService = moveService;
            _playerDataProxy = playerDataProxy;
        }

        public override void Tick(float deltaTime)
        {
            _moveService.Move(_playerDataProxy, _playerDataProxy.Direction, deltaTime);
        }

        public override PlayerStateName GetNextState()
        {
            if (_playerDataProxy.Direction == Vector3.zero)
            {
                return PlayerStateName.Idle;
            }
            return PlayerStateName.Move;
        }
    }
}