using System.Collections.Generic;
using _MultiplayerFPS.Scripts.Components;
using _MultiplayerFPS.Scripts.Components.Health;
using _MultiplayerFPS.Scripts.Config;
using _MultiplayerFPS.Scripts.Controllers;
using _MultiplayerFPS.Scripts.Services;
using _MultiplayerFPS.Scripts.Services.Config;
using _MultiplayerFPS.Scripts.Services.InputService;
using _MultiplayerFPS.Scripts.Services.Move;
using _MultiplayerFPS.Scripts.Services.State;
using _MultiplayerFPS.Scripts.State;
using _MultiplayerFPS.Scripts.Utils.ServiceLocator;
using Mirror;
using UnityEngine;

namespace _MultiplayerFPS.Scripts
{
    public class GameNetworkPlayer : NetworkBehaviour
    {
        IInputService _inputService;
        IPickupService _pickupService;
        PlayerController _playerController;
        HudPresenter _hudPresenter;
        string _lobbyPlayerNickname;
        Color _lobbyPlayerColor;
        [SerializeField] CharacterController _characterController;
        [SerializeField] GameNetworkPlayerView _gameNetworkPlayerView;
        [SerializeField] Weapon _weapon;
        [SerializeField] Transform _cameraTarget;
        [SerializeField] PlayerState _playerState;
        [SerializeField] Health _health;
        [SerializeField] GameObject _disableOnDeath;
        [SerializeField] HudView _hudViewPrefab;
        [SerializeField] PickupCollector _pickupCollector;
        
        public override void OnStartServer()
        {
            var stateService = ServiceLocator.Current.Get<IStateService>();
            stateService.GameState.PlayerStates.TryAdd(netId, _playerState);
            _playerState.Nickname = _lobbyPlayerNickname;
            _playerState.Color = _lobbyPlayerColor;
            
            _pickupService = ServiceLocator.Current.Get<IPickupService>();
            
            _health.ServerCurrentHpChanged += HealthOnServerCurrentHpChanged;
            _health.ServerIsDeadChanged += HealthOnServerIsDeadChanged;
            _health.MaxHp = ServiceLocator.Current.Get<IConfigService>().Get<PlayerConfig>().MaxHealth;
            _health.FullHeal();
        }

        public override void OnStopServer()
        {
            _health.ServerCurrentHpChanged -= HealthOnServerCurrentHpChanged;
            _health.ServerIsDeadChanged -= HealthOnServerIsDeadChanged;
        }

        [Server]
        public void Init(string nickname, Color color)
        {
            _lobbyPlayerNickname = nickname;
            _lobbyPlayerColor = color;
        }
        public override void OnStartClient()
        {
            if (!isLocalPlayer) return;
            
            var inputService = new MouseKeyboardInputService();
            ServiceLocator.Current.Register<IInputService>(inputService);
            var moveService = new CharacterControllerPlayerMovementService(_characterController, _playerState);
            ServiceLocator.Current.Register<IPlayerMovementService>(moveService);
            var cameraManager = new CameraManager(Camera.main);
            ServiceLocator.Current.Register<ICameraManager>(cameraManager);
            
            _playerController = new PlayerController(_weapon, _cameraTarget, 
                _characterController, _health, _playerState);
            
            var hudView = Instantiate(_hudViewPrefab);
            _hudPresenter = new HudPresenter(_weapon, _playerState, hudView);
            _pickupCollector.PickupCollected += PickupCollectorOnPickupCollected;
        }

        public override void OnStopClient()
        {
            if (!isLocalPlayer) return;
            _pickupCollector.PickupCollected -= PickupCollectorOnPickupCollected;
        }

        public override void OnStopLocalPlayer()
        {
            ServiceLocator.Current.Unregister<IInputService>();
            ServiceLocator.Current.Unregister<IPlayerMovementService>();
            ServiceLocator.Current.Unregister<ICameraManager>();
        }
        void Update()
        {
            if (!isLocalPlayer) return;
            _playerController.Update();
            
            if (_playerState.IsDead) return;
            _gameNetworkPlayerView.HandleRunAnimations();
        }
        void LateUpdate()
        {
            if (!isLocalPlayer) return;
            _playerController.UpdateCamera();
        }
        void HealthOnServerIsDeadChanged(bool obj)
        {
            _playerState.IsDead = obj;
            _disableOnDeath.SetActive(!obj);
        }
        void HealthOnServerCurrentHpChanged(int oldHp, int newHp)
        {
            _playerState.CurrentHealth = newHp;
        }
        
        void PickupCollectorOnPickupCollected(uint netId)
        {
            CmdTryPickUp(netId);
        }
        [Command]
        public void CmdTryPickUp(uint netId)
        {
            _pickupService.TryPickup(netId, connectionToClient.identity.netId);
        }
    }
}
