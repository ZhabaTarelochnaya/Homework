using _TopDownShooter.Scripts.Gameplay.Services;
using _TopDownShooter.Scripts.Utils.StateMachine;

namespace _TopDownShooter.Scripts.Gameplay.Controllers.ShootStates
{
    public class IdleState : FSMState<WeaponStateName> 
    {
        readonly InputService _inputService;
        readonly WeaponManager _weaponManager;

        public IdleState(InputService inputService, WeaponManager weaponManager) : base(WeaponStateName.Idle)
        {
            _inputService = inputService;
            _weaponManager = weaponManager;
        }

        public override WeaponStateName GetNextState()
        {
            if (_weaponManager.CurrentAmmo < _weaponManager.MaxAmmo)
            {
                return WeaponStateName.Reload;
            } 
            if (_inputService.IsShooting())
            {
                return WeaponStateName.Shoot;
            }
            return WeaponStateName.Idle;
        }
    }
}