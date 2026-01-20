using _TopDownShooter.Scripts.Gameplay.Services;
using _TopDownShooter.Scripts.Utils.ServiceLocator;
using _TopDownShooter.Scripts.Utils.StateMachine;
using UnityEngine;

namespace _TopDownShooter.Scripts.Gameplay.Controllers.MoveStates
{
    public class IdleState : FSMState<PlayerMoveStateName>
    {
        readonly Rigidbody _rigidbody;
        readonly MoveService _moveService;
        readonly InputService _inputService;

        public IdleState(Rigidbody rigidbody) : base(PlayerMoveStateName.Idle)
        {
            _rigidbody = rigidbody;
            _moveService = ServiceLocator.Current.Get<MoveService>();
            _inputService = ServiceLocator.Current.Get<InputService>();
        }

        public override void OnEnter()
        {
            _moveService.Stop(_rigidbody);
        }

        public override PlayerMoveStateName GetNextState()
        {
            if (_inputService.GetMoveDirection() != Vector3.zero)
            {
                return PlayerMoveStateName.Move;
            }
            return PlayerMoveStateName.Idle;    
        }
    }
}