using System;
using System.Collections.Generic;
using System.Linq;
using _MultiplayerFPS.Scripts.Components;
using _MultiplayerFPS.Scripts.Config;
using _MultiplayerFPS.Scripts.Config.Pickup;
using _MultiplayerFPS.Scripts.Services.Config;
using _MultiplayerFPS.Scripts.Services.State;
using _MultiplayerFPS.Scripts.Utils.ServiceLocator;
using Mirror;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _MultiplayerFPS.Scripts.Services
{
    public class PickupService : IPickupService
    {
        readonly Transform[] _spawnPoints;
        readonly IStateService _stateService;
        readonly PlayerConfig _playerConfig;
        readonly Array _allNames;
        readonly Dictionary<PickupName, ObjectPool<Pickup>> _pool;
        

        public PickupService(Transform[] spawnPoints, IStateService stateService)
        {
            _spawnPoints = spawnPoints;
            _stateService = stateService;
            var configService = ServiceLocator.Current.Get<IConfigService>();
            _playerConfig = configService.Get<PlayerConfig>();
            _pool = configService
                .GetAll<PickupConfig>()
                .ToDictionary(c => c.Name, c => new ObjectPool<Pickup>(c.Prefab));
            _allNames = Enum.GetValues(typeof(PickupName));
        }

        public void Spawn(PickupName name)
        {
            var spawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Length)];
            Debug.Log("Spawning Pickup, pool.Count: " + _pool[name].Count);
            if (_pool[name].Get(out Pickup pickup))
            {
                NetworkServer.Spawn(pickup.gameObject);
            }
            else
            {
                pickup.RpcSetActive(true);
                pickup.IsPickedUp = false;
            }
            pickup.transform.position = spawnPoint.transform.position;
            _stateService.GameState.ActivePickups.Add(pickup.netId, pickup);
        }

        public void SpawnRandom()
        {
            var rngName = (PickupName)_allNames.GetValue(Random.Range(0, _allNames.Length));
            Spawn(rngName);
        }

        public bool TryPickup(uint itemNetId, uint playerNetId)
        {
            if (!_stateService.GameState.ActivePickups.TryGetValue(itemNetId, out var pickup)) return false;
            
            var player = _stateService.GetPlayerState(playerNetId);
            var range = _playerConfig.PickupRange;
            bool isInRange = (player.Position - pickup.transform.position).sqrMagnitude <= range * range;
            if (!isInRange || pickup.IsPickedUp) return false; 
            
            pickup.IsPickedUp = true;
            player.Pickups.Add(pickup.Name);
            pickup.RpcSetActive(false);
            _stateService.GameState.ActivePickups.Remove(itemNetId);
            _pool[pickup.Name].Return(pickup);
            return true;
        }
    }
}