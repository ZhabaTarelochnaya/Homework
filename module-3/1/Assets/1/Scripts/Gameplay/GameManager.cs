using System.Collections;
using _1.Gameplay.Data;
using UnityEngine;

namespace _1.Gameplay
{
    public class GameManager
    {
        readonly SpawnZone _spawnZone;
        readonly GameplayDataProxy _gameplayDataProxy;
        int _spawnCount;
        public float SpawnCoolDown = 4f;
        public GameManager(SpawnZone spawnZone, GameplayDataProxy gameplayDataProxy)
        {
            _spawnZone = spawnZone;
            _gameplayDataProxy = gameplayDataProxy;
            _spawnZone.Spawned += OnSpawn;
        }

        void OnSpawn(GameObject obj)
        {
            if (obj.TryGetComponent(out Target target))
            {
                target.Bind(_gameplayDataProxy);
            }
            else
            {
                Debug.LogError($"Spawned object {obj.name} is not a target");
            }
        }

        public IEnumerator Run()
        {
            while (true)
            {
                _spawnZone.Spawn();
                Debug.Log($"Targets spawned:{++_spawnCount}");
                yield return new WaitForSeconds(SpawnCoolDown);
            }
        }
    }
}