using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _MultiplayerFPS.Scripts.Services.Respawn
{
    public class RandomRespawnService : IRespawnService
    {
        readonly Transform[] _startPositions;
        public RandomRespawnService(Transform[] startPositions)
        {
            _startPositions = startPositions;
        }

        public event Action Respawned;

        public void Respawn(CharacterController player)
        {
            var spawnPoint = GetSpawnPoint();
            player.enabled = false;
            player.transform.SetPositionAndRotation(
                spawnPoint.position,
                spawnPoint.rotation
            );
            player.enabled = true;
        }
        
        Transform GetSpawnPoint() => _startPositions[Random.Range(0, _startPositions.Length)];
    }
}