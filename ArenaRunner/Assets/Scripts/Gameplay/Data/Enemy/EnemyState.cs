

using DefaultNamespace.Gameplay.World;
using UnityEngine;

namespace DefaultNamespace.Gameplay.Data.Enemy
{
    public class EnemyState : IMovable
    {
        public EnemyType Type { get; }
        public int ID { get; }
        public float Speed { get; set; }
        public Vector3 Position { get; set; } 
        public Vector3 Velocity { get; set; }

        public EnemyState(EnemyData enemyData)
        {
            Type = enemyData.Type;
            ID = enemyData.ID;
            Speed = enemyData.Speed;
            Position = enemyData.Position;
        }

        public EnemyData ToData()
        {
            var enemyData = new EnemyData();
            enemyData.Type = Type;
            enemyData.ID = ID;
            enemyData.Speed = Speed;
            enemyData.Position = Position;
            return enemyData;
        }
    }
}