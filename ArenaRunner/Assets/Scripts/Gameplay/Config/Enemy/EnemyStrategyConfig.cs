using Gameplay.Data.Enemy;
using UnityEngine;

namespace DefaultNamespace.Gameplay.Data
{
    public abstract class EnemyStrategyConfig : ScriptableObject
    {
        public abstract EnemyStrategyName  StrategyName { get; }
    }
}