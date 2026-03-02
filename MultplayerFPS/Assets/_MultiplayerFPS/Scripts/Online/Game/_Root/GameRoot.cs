using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _MultiplayerFPS.Scripts.Components;
using _MultiplayerFPS.Scripts.Config;
using _MultiplayerFPS.Scripts.Services;
using _MultiplayerFPS.Scripts.Services.Config;
using _MultiplayerFPS.Scripts.Services.GrenadeService;
using _MultiplayerFPS.Scripts.Services.LoggerService;
using _MultiplayerFPS.Scripts.Services.Respawn;
using _MultiplayerFPS.Scripts.Services.State;
using _MultiplayerFPS.Scripts.State;
using _MultiplayerFPS.Scripts.Utils.ServiceLocator;
using Mirror;
using UnityEngine;

namespace _MultiplayerFPS.Scripts
{
    public class GameRoot : NetworkBehaviour
    {
        WaitForSeconds _waitForRespawn;
        ILoggerService _loggerService;
        IPickupService _pickupService;
        GameConfig _gameConfig;
        [SerializeField] GameState _gameState;
        [SerializeField] Transform[] _spawnPoints;
        [SerializeField] Transform[] _pickUpSpawnPoints;

        public override void OnStartServer()
        {
            var stateService = new StateService(_gameState);
            ServiceLocator.Current.Register<IStateService>(stateService);
            _pickupService = new PickupService(_pickUpSpawnPoints, stateService);
            ServiceLocator.Current.Register(_pickupService);
            var grenadeService = new GrenadeService();
            ServiceLocator.Current.Register<IGrenadeService>(grenadeService);
            var playerScoreService = new PlayerScoreService(stateService);
            ServiceLocator.Current.Register<IPlayerScoreService>(playerScoreService);
            
            _gameConfig = ServiceLocator.Current.Get<IConfigService>().Get<GameConfig>();
            
            _waitForRespawn = new WaitForSeconds(_gameConfig.PickupRespawnTime);
            for (int i = 0; i < _gameConfig.MaxPickups; i++)
            {
                _pickupService.SpawnRandom();
            }
            
            _gameState.ActivePickups.OnRemove += OnRemove;
        }
        public override void OnStopServer()
        {
            ServiceLocator.Current.Unregister<IStateService>();
            ServiceLocator.Current.Unregister<IPickupService>();
            ServiceLocator.Current.Unregister<IGrenadeService>();
            ServiceLocator.Current.Unregister<IPlayerScoreService>();
            
            _gameState.ActivePickups.OnRemove -= OnRemove;
        }
        public override void OnStartClient()
        {
            if (!isServer)
            {
                var stateService = new StateService(_gameState);
                ServiceLocator.Current.Register<IStateService>(stateService);
            }
            var respawnService = new RandomRespawnService(_spawnPoints);
            ServiceLocator.Current.Register<IRespawnService>(respawnService);
            
            Cursor.lockState = CursorLockMode.Locked;
            _loggerService = ServiceLocator.Current.Get<ILoggerService>();
            _loggerService.Log("Match started");
        }
        public override void OnStopClient()
        {
            if (!isServer)
            {
                ServiceLocator.Current.Unregister<IStateService>();
            }
            ServiceLocator.Current.Unregister<IRespawnService>();
        }
        
        void OnRemove(uint arg1, Pickup arg2)
        {
            if (isServer && _gameConfig.DoPickupsRespawn)
            {
                StartCoroutine(RespawnPickup());
            }
        }
        IEnumerator RespawnPickup()
        {
            yield return _waitForRespawn;
            _pickupService.SpawnRandom();
        }
    }
}