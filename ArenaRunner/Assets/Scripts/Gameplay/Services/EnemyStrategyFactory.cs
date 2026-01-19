using System.Collections.Generic;
using DefaultNamespace.Gameplay.Controllers.Strategies;
using DefaultNamespace.Gameplay.Data;
using DefaultNamespace.Gameplay.Data.Enemy;
using DefaultNamespace.Gameplay.Data.Strategies;
using Gameplay.Data.Enemy;
using Utils.ServiceLocator;

namespace DefaultNamespace.Gameplay.Controllers
{
    public class EnemyStrategyFactory : IService
    {
        Dictionary<EnemyType, EnemyStrategyConfig> _strategyConfigs = new();
        public EnemyStrategyFactory(GameConfig gameConfig)
        {
            foreach (var enemyConfig in gameConfig.EnemyConfigs)
            {
                _strategyConfigs.Add(enemyConfig.Type, enemyConfig.StrategyConfig);
            }
        }
        public IEnemyStrategy Create(EnemyState enemyState)
        {
            switch (enemyState.StrategyName)
            {
                case EnemyStrategyName.ChaseStrategy:
                    return new ChaseStrategy((ChaseStrategyConfig)_strategyConfigs[enemyState.Type], enemyState);
                case EnemyStrategyName.PatrolStrategy:
                    return new PatrolStrategy((PatrolStrategyConfig)_strategyConfigs[enemyState.Type], enemyState);
                default:
                    return null;
            }
        }
    }
}