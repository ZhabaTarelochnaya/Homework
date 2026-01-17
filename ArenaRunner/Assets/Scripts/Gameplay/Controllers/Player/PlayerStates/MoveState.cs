using DefaultNamespace.Gameplay.Controllers;
using DefaultNamespace.Gameplay.Data;
using UnityEngine;
using Utils.FiniteStateMachine;
using Utils.ServiceLocator;

namespace DefaultNamespace.Gameplay.World.PlayerStates
{
    public class MoveState : FSMState<PlayerStateName>
    {
        readonly MoveService _moveService;
        readonly PlayerState _playerState;
        readonly CameraManager _cameraManager;

        public MoveState(MoveService moveService, PlayerState playerState) 
            : base(PlayerStateName.Move)
        {
            _moveService = moveService;
            _playerState = playerState;
            _cameraManager = ServiceLocator.Current.Get<CameraManager>();
        }

        public override void Tick(float deltaTime)
        {
            _moveService.Move(_playerState, _playerState.InputMoveDirection, deltaTime);
        }

        public override PlayerStateName GetNextState()
        {
            if (_playerState.InputMoveDirection == Vector2.zero)
            {
                return PlayerStateName.Idle;
            }
            return PlayerStateName.Move;
        }
        
    }
}