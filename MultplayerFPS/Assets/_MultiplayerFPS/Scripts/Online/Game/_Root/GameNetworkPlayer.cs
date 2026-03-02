using System.Collections;
using System.Collections.Generic;
using _MultiplayerFPS.Scripts.Components;
using _MultiplayerFPS.Scripts.Components.Health;
using _MultiplayerFPS.Scripts.Config;
using _MultiplayerFPS.Scripts.Config.Pickup;
using _MultiplayerFPS.Scripts.Controllers;
using _MultiplayerFPS.Scripts.Leaderboard;
using _MultiplayerFPS.Scripts.Services;
using _MultiplayerFPS.Scripts.Services.Config;
using _MultiplayerFPS.Scripts.Services.GrenadeService;
using _MultiplayerFPS.Scripts.Services.InputService;
using _MultiplayerFPS.Scripts.Services.Move;
using _MultiplayerFPS.Scripts.Services.ServerCommands;
using _MultiplayerFPS.Scripts.Services.State;
using _MultiplayerFPS.Scripts.State;
using _MultiplayerFPS.Scripts.Utils;
using _MultiplayerFPS.Scripts.Utils.ServiceLocator;
using Mirror;
using UnityEngine;

namespace _MultiplayerFPS.Scripts
{
    public class GameNetworkPlayer : NetworkBehaviour
    {
        IInputService _inputService;
        IPickupService _pickupService;
        IGrenadeService _grenadeService;
        IPlayerScoreService _playerScoreService;
        IStateService _stateService;
        PlayerController _playerController;
        HudPresenter _hudPresenter;
        LeaderboardPresenter _leaderboardPresenter;
        string _lobbyPlayerNickname;
        Color _lobbyPlayerColor;
        [SerializeField] GameNetworkPlayerView _gameNetworkPlayerView;
        [SerializeField] GameObject _disableOnDeath;
        [SerializeField] HudView _hudViewPrefab;
        [SerializeField] LeaderboardView _leaderboardView;
        [SerializeField] PickupCollector _pickupCollector;
        
        [field: SerializeField] public CharacterController CharacterController { get; private set; }
        [field: SerializeField] public Weapon Weapon { get; private set; }
        [field: SerializeField] public Transform CameraTarget { get; private set; }
        [field: SerializeField] public PlayerState PlayerState { get; private set; }
        [field: SerializeField] public Health Health { get; private set; }
        public IPresenter LeaderboardPresenter => _leaderboardPresenter;
        

        [Server]
        public void Init(string nickname, Color color)
        {
            _lobbyPlayerNickname = nickname;
            _lobbyPlayerColor = color;
        }
        public override void OnStartServer()
        {
            _grenadeService = ServiceLocator.Current.Get<IGrenadeService>();
            _pickupService = ServiceLocator.Current.Get<IPickupService>();
            _playerScoreService = ServiceLocator.Current.Get<IPlayerScoreService>();
            
            _stateService = ServiceLocator.Current.Get<IStateService>();
            _stateService.GameState.PlayerStates.TryAdd(netId, PlayerState);
            _stateService.GameState.PlayerScores.TryAdd(netId, new PlayerScore());
            _stateService.GameState.GameStateChanged += GameStateOnGameStateChanged;
            
            PlayerState.Nickname = _lobbyPlayerNickname;
            PlayerState.Color = _lobbyPlayerColor;
            Health.ServerCurrentHpChanged += HealthOnServerCurrentHpChanged;
            Health.ServerIsDeadChanged += HealthOnServerIsDeadChanged;
            Health.MaxHp = ServiceLocator.Current.Get<IConfigService>().Get<PlayerConfig>().MaxHealth;
            Health.FullHeal();
        }
        public override void OnStopServer()
        {
            _stateService.GameState.GameStateChanged -= GameStateOnGameStateChanged;
            Health.ServerCurrentHpChanged -= HealthOnServerCurrentHpChanged;
            Health.ServerIsDeadChanged -= HealthOnServerIsDeadChanged;
            _stateService.GameState.PlayerStates.Remove(netId);
            _stateService.GameState.PlayerScores.Remove(netId);
        }
        public override void OnStartClient()
        {
            if (!isLocalPlayer) return;
            
            var inputService = new MouseKeyboardInputService();
            ServiceLocator.Current.Register<IInputService>(inputService);
            var moveService = new CharacterControllerPlayerMovementService(CharacterController, PlayerState);
            ServiceLocator.Current.Register<IPlayerMovementService>(moveService);
            var cameraManager = new CameraManager(Camera.main);
            ServiceLocator.Current.Register<ICameraManager>(cameraManager);
            var playerCommandsService = new PlayerCommandsService(this);
            ServiceLocator.Current.Register<IPlayerCommandsService>(playerCommandsService);
            
            _playerController = new PlayerController(this);
            
            var hudView = Instantiate(_hudViewPrefab);
            _hudPresenter = new HudPresenter(Weapon, PlayerState, hudView);
            var leaderBoardView = Instantiate(_leaderboardView);
            _leaderboardPresenter =  new LeaderboardPresenter(leaderBoardView);
            
            _pickupCollector.PickupCollected += PickupCollectorOnPickupCollected;
            
            _playerController.SetActive(false);
            _hudPresenter.Disable();
            PlayerState.CmdChangeInitialized(true);
        }
        void Update()
        {
            if (!isLocalPlayer) return;
            _playerController.Update();
            
            if (PlayerState.IsDead) return;
            _gameNetworkPlayerView.HandleRunAnimations();
        }
        void LateUpdate()
        {
            if (!isLocalPlayer) return;
            _playerController.UpdateCamera();
        }
        void PickupCollectorOnPickupCollected(uint netId)
        {
            CmdTryPickUp(netId);
        }
        [Command]
        void CmdTryPickUp(uint netId)
        {
            _pickupService.TryPickup(netId, connectionToClient.identity.netId);
        }
        [Command]
        public void CmdThrowGrenade(Vector3 direction)
        {
            _grenadeService.ThrowGrenade(PlayerState, transform.position, direction);
        }
        [Command]
        public void CmdReturnToLobby()
        {
            _stateService.GameState.GameStateName = GameStateName.Unregister;
            NetManager.singleton.ServerChangeScene("Lobby");
        }

        void HealthOnServerIsDeadChanged(bool obj)
        {
            PlayerState.IsDead = obj;
            _disableOnDeath.SetActive(!obj);
            if (obj)
            {
                _playerScoreService.AddDeath(netId);
            }
        }
        void HealthOnServerCurrentHpChanged(int oldHp, int newHp) => PlayerState.CurrentHealth = newHp;

        void GameStateOnGameStateChanged(GameStateName state)
        {
            if (state == GameStateName.MatchGoing)
            {
                RpcEnable();
            }
            else if (state == GameStateName.MatchEnded)
            {
                RpcDisable();
            }
            else if (state == GameStateName.Unregister)
            {
                RpcUnregister(connectionToClient);
            }
        }

        [TargetRpc]
        void RpcUnregister(NetworkConnectionToClient conn)
        {
            ServiceLocator.Current.Unregister<IInputService>();
            ServiceLocator.Current.Unregister<IPlayerMovementService>();
            ServiceLocator.Current.Unregister<ICameraManager>();
            ServiceLocator.Current.Unregister<IPlayerCommandsService>();
            _pickupCollector.PickupCollected -= PickupCollectorOnPickupCollected;
        }

        [ClientRpc]
        void RpcEnable()
        {
            if (isLocalPlayer)
            {
                _hudPresenter.Enable();
                _playerController.SetActive(true);
            }
            _disableOnDeath.gameObject.SetActive(true);
        }
        [ClientRpc]
        void RpcDisable()
        {
            if (isLocalPlayer)
            {
                _playerController.SetActive(false);
                _hudPresenter.Disable();
                _leaderboardPresenter.Enable();
            }
            _disableOnDeath.gameObject.SetActive(false);
        }
    }
}
