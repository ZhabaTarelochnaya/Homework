using _TopDownShooter.Scripts.Gameplay.Configs;
using _TopDownShooter.Scripts.Gameplay.Services;
using _TopDownShooter.Scripts.Utils.ServiceLocator;
using _TopDownShooter.Scripts.Utils.StateMachine;
using UnityEngine;

namespace _TopDownShooter.Scripts.Gameplay.Controllers.MoveStates
{
    public class MoveState : FSMState<PlayerMoveStateName>
    {
        readonly Rigidbody _rigidbody;
        readonly PlayerConfig _playerConfig;
        readonly MoveService _moveService;
        readonly InputService _inputService;

        public MoveState(Rigidbody rigidbody, PlayerConfig playerConfig, InputService inputService) 
            : base(PlayerMoveStateName.Move)
        {
            _rigidbody = rigidbody;
            _playerConfig = playerConfig;
            _moveService = ServiceLocator.Current.Get<MoveService>();
            _inputService = inputService;
        }

        public override void Tick(float deltaTime)
        {
            var direction = _inputService.GetMoveDirection(); 
            _moveService.CameraRelativeMoveToDirection(_rigidbody, direction, _playerConfig.Speed);
           
        }

        public override PlayerMoveStateName GetNextState()
        {
            if (_inputService.GetMoveDirection() == Vector3.zero)
            {
                return PlayerMoveStateName.Idle;
            }
            return PlayerMoveStateName.Move;
        }
    }
}