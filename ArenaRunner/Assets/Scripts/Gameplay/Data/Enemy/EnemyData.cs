using System;
using Utils;

namespace DefaultNamespace.Gameplay.Data.Enemy
{
    [Serializable]
    public class EnemyData
    {
        public int ID;
        public float Speed;
        public SerializableVector3 Position;
        public EnemyType Type;
    }
}