using _TopDownShooter.Scripts.Gameplay.Services;
using _TopDownShooter.Scripts.Utils.StateMachine;

namespace _TopDownShooter.Scripts.Gameplay.Controllers.ShootStates
{
    public class ShootState : FSMState<WeaponStateName>
    {
        readonly InputService _inputService;
        readonly WeaponManager _weaponManager;

        public ShootState(InputService inputService, WeaponManager weaponManager) : base(WeaponStateName.Shoot)
        {
            _inputService = inputService;
            _weaponManager = weaponManager;
        }

        public override void Tick(float deltaTime)
        {
            if (_weaponManager.ShootTimer > 1 / _weaponManager.CurrentConfig.FireRate)
            {
                _weaponManager.Shoot();
            }
        }

        public override WeaponStateName GetNextState()
        {
            if (!_inputService.IsShooting())
            {
                return WeaponStateName.Idle;
            }
            if (_weaponManager.CurrentAmmo <= 0)
            {
                return WeaponStateName.Reload;
            }
            return WeaponStateName.Shoot;
        }
    }
}