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
        readonly PlayerState _playerState;
        readonly MoveService _moveService;
        public EnemyController(EnemyState enemyState)
        {
            _enemyState = enemyState;
            _playerState = ServiceLocator.Current.Get<PlayerStateService>().PlayerState;
            _moveService = ServiceLocator.Current.Get<MoveService>();
        }

        public void FixedUpdate()
        {
            _moveService.MoveTowards(_enemyState, _playerState.Position, Time.fixedDeltaTime);
        }
    }
}