using _TopDownShooter.Scripts.Gameplay.Configs;
using _TopDownShooter.Scripts.Gameplay.Controllers.MoveStates;
using _TopDownShooter.Scripts.Gameplay.Controllers.ShootStates;
using _TopDownShooter.Scripts.Gameplay.Services;
using _TopDownShooter.Scripts.Utils.EventBus;
using _TopDownShooter.Scripts.Utils.ServiceLocator;
using _TopDownShooter.Scripts.Utils.StateMachine;
using _TopDownShooter.Scripts.View;
using UnityEngine;
using UnityEngine.PlayerLoop;
using IdleState = _TopDownShooter.Scripts.Gameplay.Controllers.MoveStates.IdleState;

namespace _TopDownShooter.Scripts.Gameplay.Controllers
{
    public class PlayerController
    {
        readonly EventBus _eventBus;
        readonly Rigidbody _rigidbody;
        readonly AimService _aimService;
        readonly InputService _inputService;
        readonly WeaponManager _weaponManager;
        readonly PlayerConfig _config;
        FSM<PlayerMoveStateName> _moveFSM = new ();

        public PlayerController(Rigidbody rigidbody, HurtBox hurtBox)
        {
            _rigidbody = rigidbody;
            _config = ServiceLocator.Current.Get<ConfigProviderService>().GetPlayerConfig();
            _aimService = ServiceLocator.Current.Get<AimService>();
            _inputService = ServiceLocator.Current.Get<InputService>();
            _weaponManager = ServiceLocator.Current.Get<WeaponManager>();
            
            _moveFSM.AddState(new IdleState(_rigidbody, _inputService))
                .AddState(new MoveState(_rigidbody,  _config, _inputService));

            _eventBus = ServiceLocator.Current.Get<EventBus>();
            hurtBox.Hit += (damage, health) => _eventBus.TriggerEvent(
                new GameEvent(EventName.PlayerHurt,
                    $"Player hurt. Current health: {health}",
                    damage, hurtBox));
        }


        public void FixedUpdate()
        {
            _moveFSM.Tick(Time.fixedDeltaTime);
            _aimService.CameraRelativeAim(_rigidbody, _inputService.GetMousePosition());
        }

        public void Update()
        {
            var scroll = _inputService.SwitchWeapon();
            if (scroll != 0)
            {
                _weaponManager.SwitchWeapon(scroll);
            }
        }
    }
}