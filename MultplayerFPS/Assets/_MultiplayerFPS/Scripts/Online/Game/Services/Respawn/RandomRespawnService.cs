using UnityEngine;

namespace _MultiplayerFPS.Scripts.Services.Respawn
{
    public class RandomRespawnService : IRespawnService
    {
        readonly Transform[] _startPositions;
        public RandomRespawnService(Transform[] startPositions)
        {
            _startPositions = startPositions;
        }
        public void Respawn(CharacterController player)
        {
            Transform spawnPoint = GetNextSpawnPoint();
            player.enabled = false;
            player.transform.SetPositionAndRotation(
                spawnPoint.position,
                spawnPoint.rotation
            );
            player.enabled = true;
        }
        Transform GetNextSpawnPoint() => _startPositions[Random.Range(0, _startPositions.Length)];
    }
}