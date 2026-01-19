using Gameplay.Data.Enemy;
using UnityEngine;

namespace DefaultNamespace.Gameplay.Data.Strategies
{
    [CreateAssetMenu(fileName = "ChaseStrategyConfig", menuName = "ScriptableObjects/EnemyStrategies/ChaseStrategyConfig")]
    public class ChaseStrategyConfig : EnemyStrategyConfig
    {
        public override EnemyStrategyName StrategyName => EnemyStrategyName.ChaseStrategy;
    }
}