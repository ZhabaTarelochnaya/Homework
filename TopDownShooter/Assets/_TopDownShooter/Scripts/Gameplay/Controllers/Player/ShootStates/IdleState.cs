using _TopDownShooter.Scripts.Gameplay.Services;
using _TopDownShooter.Scripts.Utils.StateMachine;

namespace _TopDownShooter.Scripts.Gameplay.Controllers.ShootStates
{
    public class IdleState : FSMState<PlayerShootStateName> 
    {
        readonly InputService _inputService;

        public IdleState(InputService inputService) : base(PlayerShootStateName.Idle)
        {
            _inputService = inputService;
        }

        public override PlayerShootStateName GetNextState()
        {
            if (_inputService.IsShooting())
            {
                return PlayerShootStateName.Shoot;
            }
            return PlayerShootStateName.Idle;
        }
    }
}