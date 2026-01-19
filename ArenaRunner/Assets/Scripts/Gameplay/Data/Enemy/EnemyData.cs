using System;
using Gameplay.Data.Enemy;
using Utils;

namespace DefaultNamespace.Gameplay.Data.Enemy
{
    [Serializable]
    public class EnemyData
    {
        public int ID;
        public float Speed;
        public SerializableVector2 Position;
        public EnemyType Type;
        public EnemyStrategyName StrategyName;
        public int Damage;
    }
}