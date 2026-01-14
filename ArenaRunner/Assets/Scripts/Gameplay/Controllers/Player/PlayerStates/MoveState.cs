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
        readonly PlayerDataProxy _playerDataProxy;
        readonly CameraManager _cameraManager;

        public MoveState(MoveService moveService, PlayerDataProxy playerDataProxy) 
            : base(PlayerStateName.Move)
        {
            _moveService = moveService;
            _playerDataProxy = playerDataProxy;
            _cameraManager = ServiceLocator.Current.Get<CameraManager>();
        }

        public override void Tick(float deltaTime)
        {
            var inputDir = _playerDataProxy.InputMoveDirection;
            var yRotation = _cameraManager.CurrentCamera.Rotation.y;
            var relativeDirection = _moveService.RotateAroundYAxis(inputDir, yRotation);
            _moveService.Move(_playerDataProxy, relativeDirection, deltaTime);
        }

        public override PlayerStateName GetNextState()
        {
            if (_playerDataProxy.InputMoveDirection == Vector3.zero)
            {
                return PlayerStateName.Idle;
            }
            return PlayerStateName.Move;
        }
        
    }
}