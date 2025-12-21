using UnityEngine;

namespace NPCEventNotification.Scripts.Gameplay.World
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] GameObject _enemyPrefab;
        [SerializeField] int _minEnemySpawnAmount;
        [SerializeField] int _maxEnemySpawnAmount = 1;

        public void Bind(EventManager manager)
        {
            manager.OnGameEvent += ManagerOnGameEvent;
        }

        void ManagerOnGameEvent(GameEvent e)
        {
            if (e.Name == GameEventName.Night)
            {
                var amount = Random.Range(_minEnemySpawnAmount, _maxEnemySpawnAmount + 1);
                for (var i = 0; i < amount; i++)
                {
                    Instantiate(_enemyPrefab, transform.position, Quaternion.identity);
                }
            }
        }
    }
}