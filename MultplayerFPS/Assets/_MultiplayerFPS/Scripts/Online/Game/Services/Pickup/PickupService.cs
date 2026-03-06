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
        readonly IStateService _stateService;
        readonly PlayerConfig _playerConfig;
        readonly Array _allNames;
        readonly Dictionary<PickupName, ObjectPool<Pickup>> _pool;
        readonly Dictionary<PickupName, PickupConfig> _configs;
        readonly List<Transform> _unoccupiedSpawnPoints;
        

        public PickupService(Transform[] spawnPoints, IStateService stateService)
        {
            _unoccupiedSpawnPoints = spawnPoints.ToList();
            
            _stateService = stateService;
            
            var configService = ServiceLocator.Current.Get<IConfigService>();
            _playerConfig = configService.Get<PlayerConfig>();
            _pool = configService
                .GetAll<PickupConfig>()
                .ToDictionary(c => c.Name, c => new ObjectPool<Pickup>(c.Prefab));
            _configs = configService
                .GetAll<PickupConfig>()
                .ToDictionary(c => c.Name, c => c);
            
            _allNames = Enum.GetValues(typeof(PickupName));
        }
        
        public void Spawn(PickupName name)
        {
            var index = Random.Range(0, _unoccupiedSpawnPoints.Count);
            var spawnPoint = _unoccupiedSpawnPoints[index];
            _unoccupiedSpawnPoints.RemoveAt(index);
            
            if (_pool[name].Get(out Pickup pickup))
            {
                NetworkServer.Spawn(pickup.gameObject);
            }
            else
            {
                pickup.RpcSetActive(true);
                pickup.IsPickedUp = false;
            }
            pickup.SpawnPoint = spawnPoint;
            pickup.transform.position = spawnPoint.transform.position;
            Debug.Log(_stateService);
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
            bool isOverLimit = player.Pickups.Count(n => n == pickup.Name) >= _configs[pickup.Name].Limit;
            if (isOverLimit) return false;
            
            var range = _playerConfig.PickupRange;
            bool isOutOfRange = (player.Position - pickup.transform.position).sqrMagnitude > range * range;
            if (isOutOfRange || pickup.IsPickedUp) return false; 
            
            pickup.RpcSetActive(false);
            pickup.IsPickedUp = true;
            
            player.Pickups.Add(pickup.Name);
            _stateService.GameState.ActivePickups.Remove(itemNetId);
            
            _unoccupiedSpawnPoints.Add(pickup.SpawnPoint);
            _pool[pickup.Name].Return(pickup);
            return true;
        }
    }
}