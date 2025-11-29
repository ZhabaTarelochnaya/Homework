using System.Collections;
using UnityEngine;

namespace _1.Gameplay
{
    public class GameManager
    {
        SpawnZone _spawnZone;
        public float SpawnCoolDown = 4f;
        public GameManager(SpawnZone spawnZone)
        {
            _spawnZone = spawnZone;
        }
        public IEnumerator Run()
        {
            while (true)
            {
                _spawnZone.Spawn();
                yield return new WaitForSeconds(SpawnCoolDown);
            }
        }
    }
}