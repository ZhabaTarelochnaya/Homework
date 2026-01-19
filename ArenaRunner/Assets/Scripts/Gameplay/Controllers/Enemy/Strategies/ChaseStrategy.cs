using DefaultNamespace.Gameplay.Data;
using DefaultNamespace.Gameplay.Data.Enemy;
using DefaultNamespace.Gameplay.Data.Strategies;
using DefaultNamespace.Gameplay.World;
using Gameplay.Services;
using UnityEngine;
using Utils.ServiceLocator;

namespace DefaultNamespace.Gameplay.Controllers.Strategies
{
    public class ChaseStrategy : IEnemyStrategy
    {
        readonly EnemyState _enemyState;
        readonly MoveService _moveService;
        readonly PlayerStateService _playerStateService;

        public ChaseStrategy(ChaseStrategyConfig config, EnemyState enemyState)
        {
            _enemyState = enemyState;
            _moveService = ServiceLocator.Current.Get<MoveService>();
            _playerStateService = ServiceLocator.Current.Get<PlayerStateService>();
        }
        public void Move()
        {
            _moveService.MoveTowards(_enemyState, _playerStateService.PlayerState.Position, Time.fixedDeltaTime);
        }
    }
}