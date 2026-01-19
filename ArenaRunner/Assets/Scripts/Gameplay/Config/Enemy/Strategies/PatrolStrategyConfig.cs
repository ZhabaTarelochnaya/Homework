using Gameplay.Data.Enemy;
using UnityEditor;
using UnityEngine;

namespace DefaultNamespace.Gameplay.Data.Strategies
{
    [CreateAssetMenu(fileName = "PatrolStrategyConfig", menuName = "ScriptableObjects/EnemyStrategies/PatrolStrategyConfig")]
    public class PatrolStrategyConfig : EnemyStrategyConfig
    {
        public override EnemyStrategyName StrategyName => EnemyStrategyName.PatrolStrategy;
        [field: SerializeField] public float Distance { get; private set; }
        [field: SerializeField] public Vector2 PatrolBounds { get; private set; }
        [field: SerializeField] public Vector2 PatrolBoundsCenter { get; private set; }
    }
}