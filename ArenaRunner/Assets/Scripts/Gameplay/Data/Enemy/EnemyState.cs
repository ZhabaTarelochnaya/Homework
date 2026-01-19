

using DefaultNamespace.Gameplay.World;
using Gameplay.Data.Enemy;
using Gameplay.View.World.Enemies.HitHurtBoxes;
using UnityEngine;

namespace DefaultNamespace.Gameplay.Data.Enemy
{
    public class EnemyState : IMovable, IDamageDealer
    {
        public EnemyType Type { get; }
        public EnemyStrategyName StrategyName { get; }
        public int ID { get; }
        public float Speed { get; set; }
        public Vector2 Position { get; set; } 
        public Vector2 Velocity { get; set; }

        public int Damage { get; set; }

        public EnemyState(EnemyData enemyData)
        {
            Type = enemyData.Type;
            StrategyName = enemyData.StrategyName;
            ID = enemyData.ID;
            Speed = enemyData.Speed;
            Position = enemyData.Position;
            Damage = enemyData.Damage;
        }

        public EnemyData ToData()
        {
            var enemyData = new EnemyData();
            enemyData.Type = Type;
            enemyData.StrategyName = StrategyName;
            enemyData.ID = ID;
            enemyData.Speed = Speed;
            enemyData.Position = Position;
            enemyData.Damage = Damage;
            return enemyData;
        }
    }
}