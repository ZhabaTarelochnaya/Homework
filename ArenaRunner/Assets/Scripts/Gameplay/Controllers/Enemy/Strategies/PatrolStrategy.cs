using DefaultNamespace.Gameplay.Data.Enemy;
using DefaultNamespace.Gameplay.Data.Strategies;
using DefaultNamespace.Gameplay.World;
using UnityEngine;
using Utils.ServiceLocator;

namespace DefaultNamespace.Gameplay.Controllers.Strategies
{
    public class PatrolStrategy : IEnemyStrategy
    {
        const float StoppingDistance = 1f;
        readonly PatrolStrategyConfig _config;
        readonly EnemyState _enemyState;
        readonly MoveService _moveService;
        Vector2 _target;

        public PatrolStrategy(PatrolStrategyConfig config, EnemyState enemyState)
        {
            _config = config;
            _enemyState = enemyState;
            _moveService = ServiceLocator.Current.Get<MoveService>();
            SetTarget();
        }
        public void Move()
        {
            if ((_enemyState.Position - _target).magnitude <= StoppingDistance)
            {
                SetTarget();
            }
            _moveService.MoveTowards(_enemyState, _target, Time.fixedDeltaTime);
        }
        void SetTarget()
        {
            var direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
            _target = _enemyState.Position + direction * _config.Distance;
                
            Rect boundsRect = new Rect(
                _config.PatrolBoundsCenter.x - _config.PatrolBounds.x / 2f,
                _config.PatrolBoundsCenter.y - _config.PatrolBounds.y / 2f,
                _config.PatrolBounds.x,
                _config.PatrolBounds.y
            );
            _target = new Vector2(
                Mathf.Clamp(_target.x, boundsRect.xMin, boundsRect.xMax),
                Mathf.Clamp(_target.y, boundsRect.yMin, boundsRect.yMax)
            );
            Debug.Log(_target);
        }
    }
}