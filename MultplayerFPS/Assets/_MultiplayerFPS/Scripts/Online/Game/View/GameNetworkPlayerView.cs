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
        void OnAdd(uint netId)
        {
            var playerState = _stateService.GetPlayerState(netId);
            
            OnNicknameChanged(playerState.Nickname);
            OnColorChanged( playerState.Color);
            playerState.NicknameChanged += OnNicknameChanged;
            playerState.ColorChanged += OnColorChanged;

            if (isLocalPlayer)
            {
                var loggerService = ServiceLocator.Current.Get<ILoggerService>();
                loggerService.Log($"Player {playerState.Nickname} (netId: {netId}) joined the game.");
            }
            
            if (netId != this.netId) return;
            playerState.IsShootingChanged += PlayerStateOnIsShootingChanged;
            playerState.IsReloadingChanged += PlayerStateOnIsReloadingChanged; 
            playerState.IsDeadChanged += PlayerStateOnIsDeadChanged;
            playerState.CurrentHealthChanged += PlayerStateOnCurrentHealthChanged;
        }

        void OnRemove(uint netId, PlayerState state)
        {
            state.NicknameChanged -= OnNicknameChanged;
            state.ColorChanged -= OnColorChanged;
            
            if (netId != this.netId) return;
            state.IsShootingChanged -= PlayerStateOnIsShootingChanged;
            state.IsReloadingChanged -= PlayerStateOnIsReloadingChanged;
            state.IsDeadChanged -= PlayerStateOnIsDeadChanged;
            state.CurrentHealthChanged -= PlayerStateOnCurrentHealthChanged;
        }
        public override void OnStopClient()
        {
            var player = _stateService.GetPlayerState(netId);
            var loggerService = ServiceLocator.Current.Get<ILoggerService>();
            loggerService.Log($"Player {player.Nickname} (netId: {netId}) left the game.");
            _stateService.GameState.PlayerStates.OnAdd -= OnAdd;
            _stateService.GameState.PlayerStates.OnRemove -= OnRemove;
        }
        public void HandleRunAnimations()
        {
            var move = _inputService.GetMove();
            _animator.SetInteger(MoveVertical, (int)move.y);
            _animator.SetInteger(MoveHorizontal, (int)move.x);
        }

        void PlayerStateOnCurrentHealthChanged(int arg1, int arg2)
        {
            _nicknameTagView.SetHealth(arg2 / (float)_playerConfig.MaxHealth);
        }
        void PlayerStateOnIsDeadChanged(bool obj)
        {
            _disableOnDeath.SetActive(!obj);
        }
        void OnNicknameChanged(string newNickname) => _nicknameTagView.SetNickname(newNickname);
        void OnColorChanged(Color newColor)
        {
            _nicknameTagView.SetColor(newColor);
            _renderer.material.color = newColor;
        }
        void PlayerStateOnIsShootingChanged(bool obj)
        {
            _animator.SetBool(IsShooting, obj);
        }
        void PlayerStateOnIsReloadingChanged(bool obj) => _animator.SetBool(IsReloading, obj);
    }
}