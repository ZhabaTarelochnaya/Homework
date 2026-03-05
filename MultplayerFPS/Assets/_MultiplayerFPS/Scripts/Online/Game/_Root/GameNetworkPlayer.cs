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
using _MultiplayerFPS.Scripts.Services.Respawn;
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
        IStateService _stateService;
        IRespawnService _respawnService;
        PlayerController _playerController;
        HudPresenter _hudPresenter;
        LeaderboardPresenter _leaderboardPresenter;
        string _lobbyPlayerNickname;
        Color _lobbyPlayerColor;
        [SerializeField] GameNetworkPlayerView _gameNetworkPlayerView;
        [SerializeField] GameNetworkPlayerCommands _gameNetworkPlayerCommands;
        [SerializeField] GameObject _disableOnDeath;
        [SerializeField] HudView _hudViewPrefab;
        [SerializeField] LeaderboardView _leaderboardView;
        [SerializeField] PickupCollector _pickupCollector;
        [SerializeField] CharacterController _characterController;
        [SerializeField] Weapon _weapon;
        [SerializeField] Transform _cameraTarget;
        [SerializeField] PlayerState _playerState;
        [SerializeField] Health _health;
        [SerializeField] Collider _effectorTarget;
        

        [Server]
        public void Init(string nickname, Color color)
        {
            _lobbyPlayerNickname = nickname;
            _lobbyPlayerColor = color;
        }
        public override void OnStartServer()
        {
            _stateService = ServiceLocator.Current.Get<IStateService>();
            _stateService.GameState.PlayerStates.TryAdd(netId, _playerState);
            _stateService.GameState.PlayerScores.TryAdd(netId, new PlayerScore());
            _stateService.GameState.GameStateChanged += GameStateOnGameStateChanged;
            
            _playerState.Nickname = _lobbyPlayerNickname;
            _playerState.Color = _lobbyPlayerColor;
            _playerState.Pickups.OnChange += OnChange;
            _playerState.IsDeadChanged += PlayerStateOnIsDeadChanged;
            
            _health.MaxHp = ServiceLocator.Current.Get<IConfigService>().Get<PlayerConfig>().MaxHealth;
            _health.FullHeal();
        }
        public override void OnStopServer()
        {
            _stateService.GameState.GameStateChanged -= GameStateOnGameStateChanged;
            _playerState.Pickups.OnChange -= OnChange;
            _stateService.GameState.PlayerStates.Remove(netId);
            _stateService.GameState.PlayerScores.Remove(netId);
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
            var playerCommandsService = new PlayerCommandsService(_gameNetworkPlayerCommands);
            ServiceLocator.Current.Register<IPlayerCommandsService>(playerCommandsService);

            _respawnService = ServiceLocator.Current.Get<IRespawnService>();
            _respawnService.Respawned += RespawnServiceOnRespawned;
            
            var hudView = Instantiate(_hudViewPrefab);
            _hudPresenter = new HudPresenter(_weapon, _playerState, hudView);
            var leaderBoardView = Instantiate(_leaderboardView);
            _leaderboardPresenter =  new LeaderboardPresenter(leaderBoardView);
            
            _playerController = new PlayerController(_playerState, _weapon, _cameraTarget, _leaderboardPresenter);
            
            _pickupCollector.PickupCollected += PickupCollectorOnPickupCollected;
            _playerState.IsDeadChanged += PlayerStateOnIsDeadChanged;
            
            _playerController.SetActive(false);
            _hudPresenter.Disable();
            _gameNetworkPlayerCommands.CmdChangeInitialized(true);
        }

        void RespawnServiceOnRespawned()
        {
            // _gameNetworkPlayerCommands.CmdSetEffectorTargetActive(true);
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
        [TargetRpc]
        void RpcUnregister(NetworkConnectionToClient conn)
        {
            ServiceLocator.Current.Unregister<IInputService>();
            ServiceLocator.Current.Unregister<IPlayerMovementService>();
            ServiceLocator.Current.Unregister<ICameraManager>();
            ServiceLocator.Current.Unregister<IPlayerCommandsService>();
            _pickupCollector.PickupCollected -= PickupCollectorOnPickupCollected;
            _respawnService.Respawned -= RespawnServiceOnRespawned;
            _playerState.IsDeadChanged -= PlayerStateOnIsDeadChanged;
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
        void PickupCollectorOnPickupCollected(uint netId) => _gameNetworkPlayerCommands.CmdTryPickUp(netId);
        void PlayerStateOnIsDeadChanged(PlayerState state, bool obj)
        {
            if (!isLocalPlayer) return;
            _disableOnDeath.SetActive(!obj);
            if (obj)
            {
                _gameNetworkPlayerCommands.CmdDisableEffectorTarget();
                _health.CmdStartRevive();
                _gameNetworkPlayerCommands.CmdAddDeath();
                return;
            }

            // StartCoroutine(Respawn());
            _respawnService.Respawn(_characterController);
            _gameNetworkPlayerCommands.CmdEnableEffectorTarget(transform.position);
        }


        
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
        void OnChange(SyncList<PickupName>.Operation arg1, int arg2, PickupName arg3)
        {
            if (arg1 == SyncList<PickupName>.Operation.OP_ADD)
            {
                if (arg3 == PickupName.MedKit)
                {
                    _playerState.MedKitCount++;
                }
                else if (arg3 == PickupName.Grenade)
                {
                    _playerState.GrenadeCount++;
                }
            }
            else if (arg1 == SyncList<PickupName>.Operation.OP_REMOVEAT)
            {
                if (arg3 == PickupName.MedKit)
                {
                    _playerState.MedKitCount--;
                }
                else if (arg3 == PickupName.Grenade)
                {
                    _playerState.GrenadeCount--;
                }
            }
        }
    }
}
