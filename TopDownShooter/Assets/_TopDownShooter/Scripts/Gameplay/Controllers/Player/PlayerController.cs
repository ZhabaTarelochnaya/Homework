using _TopDownShooter.Scripts.Gameplay.Configs;
using _TopDownShooter.Scripts.Gameplay.Controllers.MoveStates;
using _TopDownShooter.Scripts.Gameplay.Services;
using _TopDownShooter.Scripts.Utils.ServiceLocator;
using _TopDownShooter.Scripts.Utils.StateMachine;
using UnityEngine;

namespace _TopDownShooter.Scripts.Gameplay.Controllers
{
    public class PlayerController
    {
        readonly Rigidbody _rigidbody;
        readonly AimService _aimService;
        readonly InputService _inputService;
        PlayerConfig _config;
        FSM<PlayerMoveStateName> _moveFSM = new ();

        public PlayerController(Rigidbody rigidbody)
        {
            _rigidbody = rigidbody;
            _config = ServiceLocator.Current.Get<ConfigProviderService>().GetPlayerConfig();
            _aimService = ServiceLocator.Current.Get<AimService>();
            _inputService = ServiceLocator.Current.Get<InputService>();
            _moveFSM.AddState(new MoveState(_rigidbody,  _config))
                .AddState(new IdleState(_rigidbody));
        }

        public void FixedUpdate()
        {
            _moveFSM.Tick(Time.fixedDeltaTime);
            _aimService.CameraRelativeAim(_rigidbody.transform, _inputService.GetMousePosition());
        }
    }
}