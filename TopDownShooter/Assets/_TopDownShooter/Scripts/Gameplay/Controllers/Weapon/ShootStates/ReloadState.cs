using _TopDownShooter.Scripts.Gameplay.Services;
using _TopDownShooter.Scripts.Utils.EventBus;
using _TopDownShooter.Scripts.Utils.ServiceLocator;
using _TopDownShooter.Scripts.Utils.StateMachine;
using Unity.VisualScripting;
using EventBus = _TopDownShooter.Scripts.Utils.EventBus.EventBus;

namespace _TopDownShooter.Scripts.Gameplay.Controllers.ShootStates
{
    public class ReloadState : FSMState<WeaponStateName>
    {
        readonly InputService _inputService;
        readonly WeaponManager _weaponManager;
        readonly EventBus _eventBus;
        float _time = 0;

        public ReloadState(InputService inputService, WeaponManager weaponManager) : base(WeaponStateName.Reload)
        {
            _inputService = inputService;
            _weaponManager = weaponManager;
            _eventBus = ServiceLocator.Current.Get<EventBus>();
        }

        public override void OnEnter()
        {
            _time = _weaponManager.CurrentConfig.ReloadTime;
            _eventBus.TriggerEvent(new GameEvent(EventName.ReloadStarted, 
                "Reloading",
                _weaponManager.CurrentConfig.ReloadTime));
        }

        public override void Tick(float deltaTime)
        {
            _time -= deltaTime;
            if (_time <= 0)
            {
                _weaponManager.Reload();
            }
        }

        public override void OnExit()
        {
            _eventBus.TriggerEvent(new GameEvent(EventName.ReloadStopped, "Reload stopped"));
        }

        public override WeaponStateName GetNextState()
        {
            if (_weaponManager.CurrentAmmo > 0 & _inputService.IsShooting())
            {
                return WeaponStateName.Shoot;
            }
            if (_weaponManager.CurrentAmmo == _weaponManager.MaxAmmo)
            {
                return WeaponStateName.Idle;
            } 
            return WeaponStateName.Reload;
            
        }
    }
}