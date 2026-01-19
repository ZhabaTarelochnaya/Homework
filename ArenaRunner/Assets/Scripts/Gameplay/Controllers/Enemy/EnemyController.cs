using DefaultNamespace.Gameplay.Data;
using DefaultNamespace.Gameplay.Data.Enemy;
using DefaultNamespace.Gameplay.World;
using Gameplay.Services;
using UnityEngine;
using Utils.ServiceLocator;

namespace DefaultNamespace.Gameplay.Controllers
{
    public class EnemyController
    {
        readonly EnemyState _enemyState;
        IEnemyStrategy _strategy;
        public EnemyController(EnemyState enemyState)
        {
            _enemyState = enemyState;
            var strategyFactory = ServiceLocator.Current.Get<EnemyStrategyFactory>();
            _strategy = strategyFactory.Create(_enemyState);
        }

        public void FixedUpdate()
        {
            _strategy.Move();
        }
    }
}