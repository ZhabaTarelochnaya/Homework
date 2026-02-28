using _MultiplayerFPS.Scripts.Components;
using _MultiplayerFPS.Scripts.Components.Health;
using _MultiplayerFPS.Scripts.Config;
using _MultiplayerFPS.Scripts.Controllers.HandsStates;
using _MultiplayerFPS.Scripts.Services.Config;
using _MultiplayerFPS.Scripts.Services.InputService;
using _MultiplayerFPS.Scripts.Services.Move;
using _MultiplayerFPS.Scripts.Services.Respawn;
using _MultiplayerFPS.Scripts.Services.State;
using _MultiplayerFPS.Scripts.State;
using _MultiplayerFPS.Scripts.Utils.FiniteStateMachine;
using _MultiplayerFPS.Scripts.Utils.ServiceLocator;
using UnityEngine;

namespace _MultiplayerFPS.Scripts.Controllers
{
    public class PlayerController
    {
        readonly Weapon _weapon;
        readonly Transform _cameraOrigin;
        readonly CharacterController _player;
        readonly Health _health;
        readonly PlayerState _playerState;
        readonly IInputService _inputService;
        readonly IPlayerMovementService _playerMovementService;
        readonly ICameraManager _cameraManager;
        readonly IStateService _stateService;
        readonly IRespawnService _respawnService;
        FSM<HandsStateName> _handsFsm =  new ();

        public PlayerController(Weapon weapon, Transform cameraOrigin, CharacterController player, 
            Health health, PlayerState playerState)
        {
            _weapon = weapon;
            _cameraOrigin = cameraOrigin;
            _player = player;
            _health = health;
            _playerState = playerState;

            _inputService = ServiceLocator.Current.Get<IInputService>();
            _playerMovementService = ServiceLocator.Current.Get<IPlayerMovementService>();
            _cameraManager = ServiceLocator.Current.Get<ICameraManager>();
            _respawnService = ServiceLocator.Current.Get<IRespawnService>();
            
            _handsFsm.AddState(new IdleState(_weapon))
                .AddState(new ShootState(_weapon))
                .AddState(new ReloadState(_weapon));
            
            health.ClientIsDeadChanged += HealthOnClientIsDeadChanged;
        }

        public void Update()
        {
            if (!_playerState.IsDead)
            {
                HandleMove();
                HandleJump();
                _handsFsm.Tick(Time.deltaTime);
            }
            HandlePlayerRotation();
        }
        public void UpdateCamera()
        {
            _cameraManager.FollowPosition(_cameraOrigin);
            var look = _inputService.GetLook();
            _cameraManager.FollowRotation(look.y, _player.transform.rotation.eulerAngles.y);
        }
        void HandlePlayerRotation()
        {
            var look = _inputService.GetLook();
            _player.transform.Rotate(Vector3.up * look.x);
        }
        void HandleJump()
        {
            if (_inputService.GetJumpButtonDown())
            {
                _playerMovementService.AddJump();
            }
        }
        void HandleMove()
        {
            var input = _inputService.GetMove();
            _playerMovementService.AddRun(input);
            _playerMovementService.Move();
        }
        void HealthOnClientIsDeadChanged(bool obj)
        {
            if (obj)
            {
                _health.CmdRevive();
                return;
            }
            _respawnService.Respawn(_player);
        }
    }
}