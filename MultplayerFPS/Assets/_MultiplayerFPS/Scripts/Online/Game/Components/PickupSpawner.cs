using System.Collections;
using _MultiplayerFPS.Scripts.Config;
using _MultiplayerFPS.Scripts.Services;
using _MultiplayerFPS.Scripts.Services.Config;
using _MultiplayerFPS.Scripts.Services.State;
using _MultiplayerFPS.Scripts.State;
using _MultiplayerFPS.Scripts.Utils.ServiceLocator;
using Mirror;
using UnityEngine;

namespace _MultiplayerFPS.Scripts.Components
{
    public class PickupSpawner : NetworkBehaviour
    {
        WaitForSeconds _waitForRespawn;
        GameState _gameState;
        GameConfig _gameConfig;
        IPickupService _pickupService;

        public override void OnStartServer()
        {
            _gameState = ServiceLocator.Current.Get<IStateService>().GameState;
            _gameConfig = ServiceLocator.Current.Get<IConfigService>().Get<GameConfig>();
            _pickupService = ServiceLocator.Current.Get<IPickupService>();
            _waitForRespawn = new WaitForSeconds(_gameConfig.PickupRespawnTime);
            
            for (int i = 0; i < _gameConfig.MaxPickups; i++)
            {
                _pickupService.SpawnRandom();
            }
            
            _gameState.ActivePickups.OnRemove += OnRemove;
        }

        public override void OnStopServer()
        {
            _gameState.ActivePickups.OnRemove -= OnRemove;
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