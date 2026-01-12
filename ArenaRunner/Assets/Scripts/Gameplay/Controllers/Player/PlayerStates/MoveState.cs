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
            var direction = RotateAroundYAxis(_playerDataProxy.InputMoveDirection,
                _cameraManager.CurrentCamera.Rotation.y);
            _moveService.Move(_playerDataProxy, direction, deltaTime);
        }

        public override PlayerStateName GetNextState()
        {
            if (_playerDataProxy.InputMoveDirection == Vector3.zero)
            {
                return PlayerStateName.Idle;
            }
            return PlayerStateName.Move;
        }
        Vector3 RotateAroundYAxis(Vector3 dir, float angleDeg)
        {
            float a = -angleDeg * Mathf.Deg2Rad;
            return new Vector3(
                dir.x * Mathf.Cos(a) - dir.z * Mathf.Sin(a),
                dir.y,
                dir.x * Mathf.Sin(a) + dir.z * Mathf.Cos(a)
            );
        }
    }
}