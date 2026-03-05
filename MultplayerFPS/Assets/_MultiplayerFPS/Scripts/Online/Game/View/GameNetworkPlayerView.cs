using System;
using _MultiplayerFPS.Scripts.Components;
using _MultiplayerFPS.Scripts.Config;
using _MultiplayerFPS.Scripts.Controllers;
using _MultiplayerFPS.Scripts.Services.Config;
using _MultiplayerFPS.Scripts.Services.InputService;
using _MultiplayerFPS.Scripts.Services.LoggerService;
using _MultiplayerFPS.Scripts.Services.State;
using _MultiplayerFPS.Scripts.State;
using _MultiplayerFPS.Scripts.Utils.ServiceLocator;
using Mirror;
using UnityEngine;

namespace _MultiplayerFPS.Scripts
{
    public class GameNetworkPlayerView : NetworkBehaviour
    {
        static readonly int MoveHorizontal = Animator.StringToHash("MoveHorizontal");
        static readonly int MoveVertical = Animator.StringToHash("MoveVertical");
        static readonly int IsReloading = Animator.StringToHash("IsReloading");
        static readonly int IsShooting = Animator.StringToHash("IsShooting");
        static readonly int IsThrowingGrenade = Animator.StringToHash("IsThrowingGrenade");
        static readonly int IsHealing = Animator.StringToHash("IsHealing");

        IInputService _inputService;
        ILoggerService _loggerService;
        IStateService _stateService;
        NicknameTagView _nicknameTagView;
        PlayerConfig _playerConfig;
        
        [SerializeField] NicknameTagView _nicknameTagViewPrefab;
        [SerializeField] Renderer _renderer;
        [SerializeField] Animator _animator;
        [SerializeField] GameObject _disableOnDeath;
        [SerializeField] PlayerState _playerState;
        
        public override void OnStartClient()
        {
            _nicknameTagView = Instantiate(_nicknameTagViewPrefab, _disableOnDeath.transform);
            _playerConfig = ServiceLocator.Current.Get<IConfigService>().Get<PlayerConfig>();
            
            if (isLocalPlayer)
            {
                _inputService = ServiceLocator.Current.Get<IInputService>();
            }
            
            _stateService = ServiceLocator.Current.Get<IStateService>();
            _stateService.GameState.GameStateChanged += GameStateOnGameStateChanged;
            
            OnNicknameChanged(_playerState.Nickname);
            OnColorChanged( _playerState.Color);
            _playerState.NicknameChanged += OnNicknameChanged;
            _playerState.ColorChanged += OnColorChanged;
            _playerState.IsShootingChanged += PlayerStateOnIsShootingChanged;
            _playerState.IsReloadingChanged += PlayerStateOnIsReloadingChanged; 
            _playerState.IsHealingChanged += PlayerStateOnIsHealingChanged;
            _playerState.IsThrowingGrenadeChanged += PlayerStateOnIsThrowingGrenadeChanged;
            _playerState.IsDeadChanged += PlayerStateOnIsDeadChanged;
            _playerState.CurrentHpChanged += PlayerStateOnCurrentHpChanged;
            
            var loggerService = ServiceLocator.Current.Get<ILoggerService>();
            loggerService.Log($"Player {_playerState.Nickname} (netId: {netId}) joined the game.");
        }
        public override void OnStopClient() => Unregister();
        void GameStateOnGameStateChanged(GameStateName obj)
        {
            if (obj != GameStateName.Unregister) return;
            RpcUnregister(connectionToClient);
        }

        [TargetRpc]
        void RpcUnregister(NetworkConnectionToClient conn) => Unregister();

        void Unregister()
        {
            _playerState.NicknameChanged -= OnNicknameChanged;
            _playerState.ColorChanged -= OnColorChanged;
            _playerState.IsShootingChanged -= PlayerStateOnIsShootingChanged;
            _playerState.IsReloadingChanged -= PlayerStateOnIsReloadingChanged;
            _playerState.IsHealingChanged -= PlayerStateOnIsHealingChanged;
            _playerState.IsThrowingGrenadeChanged -= PlayerStateOnIsThrowingGrenadeChanged;
            _playerState.IsDeadChanged -= PlayerStateOnIsDeadChanged;
            _playerState.CurrentHpChanged -= PlayerStateOnCurrentHpChanged;
            var loggerService = ServiceLocator.Current.Get<ILoggerService>();
            loggerService.Log($"Player {_playerState.Nickname} (netId: {netId}) left the game.");
        }
        public void HandleRunAnimations()
        {
            var move = _inputService.GetMove();
            _animator.SetInteger(MoveVertical, (int)move.y);
            _animator.SetInteger(MoveHorizontal, (int)move.x);
        }

        void PlayerStateOnCurrentHpChanged(int arg1, int arg2)
        {
            _nicknameTagView.SetHealth(arg2 / (float)_playerConfig.MaxHealth);
        }
        void OnNicknameChanged(string newNickname) => _nicknameTagView.SetNickname(newNickname);
        void OnColorChanged(Color newColor)
        {
            _nicknameTagView.SetColor(newColor);
            _renderer.material.color = newColor;
        }
        void PlayerStateOnIsDeadChanged(PlayerState playerState, bool obj) => _disableOnDeath.SetActive(!obj);
        void PlayerStateOnIsShootingChanged(bool obj) => _animator.SetBool(IsShooting, obj);
        void PlayerStateOnIsReloadingChanged(bool obj) => _animator.SetBool(IsReloading, obj);
        void PlayerStateOnIsThrowingGrenadeChanged(bool obj) => _animator.SetBool(IsThrowingGrenade, obj);
        void PlayerStateOnIsHealingChanged(bool obj) => _animator.SetBool(IsHealing, obj);
    }
}