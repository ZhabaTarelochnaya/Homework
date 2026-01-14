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
            var inputDir = _playerState.InputMoveDirection;
            var yRotation = _cameraManager.CurrentCamera.Rotation.eulerAngles.y;
            var relativeDirection = _moveService.RotateAroundYAxis(inputDir, yRotation);
            _moveService.Move(_playerState, relativeDirection, deltaTime);
        }

        public override PlayerStateName GetNextState()
        {
            if (_playerState.InputMoveDirection == Vector3.zero)
            {
                return PlayerStateName.Idle;
            }
            return PlayerStateName.Move;
        }
        
    }
}