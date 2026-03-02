using _MultiplayerFPS.Scripts.Components;
using _MultiplayerFPS.Scripts.Components.Health;
using _MultiplayerFPS.Scripts.Config;
using _MultiplayerFPS.Scripts.Config.Pickup;
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
        readonly GameNetworkPlayer _gameNetworkPlayer;
        readonly IInputService _inputService;
        readonly IPlayerMovementService _playerMovementService;
        readonly ICameraManager _cameraManager;
        readonly IStateService _stateService;
        readonly IRespawnService _respawnService;
        FSM<HandsStateName> _handsFsm =  new ();
        bool _isLeaderboardEnabled;
        bool _isActive;

        public PlayerController(GameNetworkPlayer gameNetworkPlayer)
        {
            _gameNetworkPlayer = gameNetworkPlayer;

            _inputService = ServiceLocator.Current.Get<IInputService>();
            _playerMovementService = ServiceLocator.Current.Get<IPlayerMovementService>();
            _cameraManager = ServiceLocator.Current.Get<ICameraManager>();
            _respawnService = ServiceLocator.Current.Get<IRespawnService>();
            
            _handsFsm.AddState(new IdleState(_gameNetworkPlayer.Weapon, _gameNetworkPlayer.PlayerState))
                .AddState(new ShootState(_gameNetworkPlayer.Weapon, _gameNetworkPlayer.PlayerState))
                .AddState(new ReloadState(_gameNetworkPlayer.Weapon, _gameNetworkPlayer.PlayerState))
                .AddState(new HealState(_gameNetworkPlayer.PlayerState))
                .AddState(new GrenadeThrowState(_gameNetworkPlayer.PlayerState, gameNetworkPlayer));
            
            _gameNetworkPlayer.Health.ClientIsDeadChanged += HealthOnClientIsDeadChanged;
        }

        public void Update()
        {
            if (!_isActive) return;
            if (!_gameNetworkPlayer.Health.IsDead)
            {
                HandleMove();
                HandleJump();
                _handsFsm.Tick(Time.deltaTime);
            }
            HandleLeaderBoard();
            HandlePlayerRotation();
        }
        public void UpdateCamera()
        {
            if (!_isActive) return;
            _cameraManager.FollowPosition(_gameNetworkPlayer.CameraTarget);
            var look = _inputService.GetLook();
            _cameraManager.FollowRotation(look.y, _gameNetworkPlayer.transform.rotation.eulerAngles.y);
        }
        public void SetActive(bool active) => _isActive = active;
        void HandlePlayerRotation()
        {
            var look = _inputService.GetLook();
            _gameNetworkPlayer.transform.Rotate(Vector3.up * look.x);
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

        void HandleLeaderBoard()
        {
            if (!_inputService.GetLeaderboardButtonDown()) return;
            if (_isLeaderboardEnabled)
            {
                _gameNetworkPlayer.LeaderboardPresenter.Disable();
                _isLeaderboardEnabled = false;
                return;
            }
            _gameNetworkPlayer.LeaderboardPresenter.Enable();
            _isLeaderboardEnabled = true;
        }
        void HealthOnClientIsDeadChanged(bool obj)
        {
            if (obj)
            {
                _gameNetworkPlayer.Health.CmdRevive();
                return;
            }
            _respawnService.Respawn(_gameNetworkPlayer.CharacterController);
        }
    }
}