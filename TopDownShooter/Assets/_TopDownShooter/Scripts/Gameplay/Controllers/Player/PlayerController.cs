using _TopDownShooter.Scripts.Gameplay.Configs;
using _TopDownShooter.Scripts.Gameplay.Controllers.MoveStates;
using _TopDownShooter.Scripts.Gameplay.Services;
using _TopDownShooter.Scripts.Utils.ServiceLocator;
using _TopDownShooter.Scripts.Utils.StateMachine;
using _TopDownShooter.Scripts.View;
using UnityEngine;

namespace _TopDownShooter.Scripts.Gameplay.Controllers
{
    public class PlayerController
    {
        readonly Rigidbody _rigidbody;
        readonly AimService _aimService;
        readonly InputService _inputService;
        readonly PlayerConfig _config;
        readonly HurtBox _hurtBox;
        FSM<PlayerMoveStateName> _moveFSM = new ();
        FSM<PlayerShootStateName> _shoottFSM = new ();
        public PlayerController(Rigidbody rigidbody, HurtBox hurtBox)
        {
            _rigidbody = rigidbody;
            _config = ServiceLocator.Current.Get<ConfigProviderService>().GetPlayerConfig();
            _aimService = ServiceLocator.Current.Get<AimService>();
            _inputService = ServiceLocator.Current.Get<InputService>();
            
            _moveFSM.AddState(new IdleState(_rigidbody, _inputService))
                .AddState(new MoveState(_rigidbody,  _config, _inputService));
            _shoottFSM.AddState(new ShootStates.IdleState(_inputService))
                .AddState(new ShootStates.ShootState(_inputService));
        }

        public void FixedUpdate()
        {
            _moveFSM.Tick(Time.fixedDeltaTime);
            _aimService.CameraRelativeAim(_rigidbody, _inputService.GetMousePosition());
        }
    }
}