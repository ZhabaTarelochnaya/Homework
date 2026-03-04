using _MultiplayerFPS.Scripts.Components;
using _MultiplayerFPS.Scripts.Components.Health;
using _MultiplayerFPS.Scripts.Config;
using _MultiplayerFPS.Scripts.Config.Pickup;
using _MultiplayerFPS.Scripts.Controllers.HandsStates;
using _MultiplayerFPS.Scripts.Leaderboard;
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
        readonly PlayerState _playerState;
        readonly Transform _cameraTarget;
        readonly LeaderboardPresenter _leaderboardPresenter;
        readonly IInputService _inputService;
        readonly IPlayerMovementService _playerMovementService;
        readonly ICameraManager _cameraManager;
        readonly IStateService _stateService;
        readonly IRespawnService _respawnService;
        FSM<HandsStateName> _handsFsm =  new ();
        bool _isLeaderboardEnabled;
        bool _isActive;

        public PlayerController(PlayerState playerState, Weapon weapon, Transform cameraTarget, 
            LeaderboardPresenter leaderboardPresenter)
        {
            _playerState = playerState;
            _cameraTarget = cameraTarget;
            _leaderboardPresenter = leaderboardPresenter;
            _inputService = ServiceLocator.Current.Get<IInputService>();
            _playerMovementService = ServiceLocator.Current.Get<IPlayerMovementService>();
            _cameraManager = ServiceLocator.Current.Get<ICameraManager>();
            
            _handsFsm.AddState(new IdleState(weapon, _playerState))
                .AddState(new ShootState(weapon, _playerState))
                .AddState(new ReloadState(weapon, _playerState))
                .AddState(new HealState())
                .AddState(new GrenadeThrowState());
        }

        public void Update()
        {
            if (!_isActive) return;
            if (!_playerState.IsDead)
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
            _cameraManager.FollowPosition(_cameraTarget);
            var look = _inputService.GetLook();
            _cameraManager.FollowRotation(look.y, _playerState.transform.rotation.eulerAngles.y);
        }
        public void SetActive(bool active) => _isActive = active;
        void HandlePlayerRotation()
        {
            var look = _inputService.GetLook();
            _playerState.transform.Rotate(Vector3.up * look.x);
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
                _leaderboardPresenter.Disable();
                _isLeaderboardEnabled = false;
                return;
            }
            _leaderboardPresenter.Enable();
            _isLeaderboardEnabled = true;
        }
    }
}