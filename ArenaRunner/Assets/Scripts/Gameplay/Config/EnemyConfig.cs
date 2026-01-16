using DefaultNamespace.Gameplay.Data.Enemy;
using DefaultNamespace.Gameplay.World;
using UnityEngine;
using Utils.ServiceLocator;

namespace DefaultNamespace.Gameplay.Data
{
    [CreateAssetMenu(fileName = "EnemyConfig", menuName = "ScriptableObjects/EnemyConfig")]
    public class EnemyConfig : ScriptableObject
    {
        [field: SerializeField] public EnemyType Type { get; private set; }
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public GameObject Prefab { get; private set; }

        public EnemyState Create()
        {
            var enemyData = new EnemyData();
            enemyData.Speed = Speed;
            enemyData.Type = Type;
            var idService = ServiceLocator.Current.Get<IDService>();
            enemyData.ID = idService.CreateID();
            var enemyState = new EnemyState(enemyData);
            return enemyState;
        }
    }
}