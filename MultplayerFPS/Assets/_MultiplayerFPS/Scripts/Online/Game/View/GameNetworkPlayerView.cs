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
        
        public override void OnStartClient()
        {
            _nicknameTagView = Instantiate(_nicknameTagViewPrefab, _disableOnDeath.transform);
            _playerConfig = ServiceLocator.Current.Get<IConfigService>().Get<PlayerConfig>();
            
            if (isLocalPlayer)
            {
                _inputService = ServiceLocator.Current.Get<IInputService>();
            }
            
            _stateService = ServiceLocator.Current.Get<IStateService>();
            foreach (var netId in _stateService.GameState.PlayerStates.Keys)
            {
                OnAdd(netId);
            }
            _stateService.GameState.PlayerStates.OnAdd += OnAdd;
            _stateService.GameState.PlayerStates.OnRemove += OnRemove;
        }
        public override void OnStopClient()
        {
            var player = _stateService.GetPlayerState(netId);
            var loggerService = ServiceLocator.Current.Get<ILoggerService>();
            loggerService.Log($"Player {player.Nickname} (netId: {netId}) left the game.");
            _stateService.GameState.PlayerStates.OnAdd -= OnAdd;
            _stateService.GameState.PlayerStates.OnRemove -= OnRemove;
        }
        void OnAdd(uint netId)
        {
            var playerState = _stateService.GetPlayerState(netId);
            
            if (isLocalPlayer)
            {
                var loggerService = ServiceLocator.Current.Get<ILoggerService>();
                loggerService.Log($"Player {playerState.Nickname} (netId: {netId}) joined the game.");
            }
            
            if (netId != this.netId) return;
            OnNicknameChanged(playerState.Nickname);
            OnColorChanged( playerState.Color);
            playerState.NicknameChanged += OnNicknameChanged;
            playerState.ColorChanged += OnColorChanged;
            playerState.IsShootingChanged += PlayerStateOnIsShootingChanged;
            playerState.IsReloadingChanged += PlayerStateOnIsReloadingChanged; 
            playerState.IsHealingChanged += PlayerStateOnIsHealingChanged;
            playerState.IsThrowingGrenadeChanged += PlayerStateOnIsThrowingGrenadeChanged;
            playerState.IsDeadChanged += PlayerStateOnIsDeadChanged;
            playerState.CurrentHpChanged += PlayerStateOnCurrentHpChanged;
        }
        void OnRemove(uint netId, PlayerState state)
        {
            if (netId != this.netId) return;
            state.NicknameChanged -= OnNicknameChanged;
            state.ColorChanged -= OnColorChanged;
            state.IsShootingChanged -= PlayerStateOnIsShootingChanged;
            state.IsReloadingChanged -= PlayerStateOnIsReloadingChanged;
            state.IsHealingChanged -= PlayerStateOnIsHealingChanged;
            state.IsThrowingGrenadeChanged -= PlayerStateOnIsThrowingGrenadeChanged;
            state.IsDeadChanged -= PlayerStateOnIsDeadChanged;
            state.CurrentHpChanged -= PlayerStateOnCurrentHpChanged;
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