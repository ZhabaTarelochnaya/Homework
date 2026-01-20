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
        readonly PlayerConfig _config;
        readonly MoveService _moveService;
        readonly InputService _inputService;

        public MoveState(Rigidbody rigidbody, PlayerConfig config) : base(PlayerMoveStateName.Move)
        {
            _rigidbody = rigidbody;
            _config = config;
            _moveService = ServiceLocator.Current.Get<MoveService>();
            _inputService = ServiceLocator.Current.Get<InputService>();
        }

        public override void Tick(float deltaTime)
        {
            var direction = _inputService.GetMoveDirection(); 
            _moveService.MoveToDirection(_rigidbody, direction, _config.Speed);
           
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