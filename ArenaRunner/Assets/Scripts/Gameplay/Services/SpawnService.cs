using System;
using System.Collections;
using System.Linq;
using DefaultNamespace.Gameplay.Controllers;
using DefaultNamespace.Gameplay.Data;
using DefaultNamespace.Gameplay.View.Enemies.Types;
using Gameplay.Services;
using UnityEngine;
using Utils.EventBus;
using Utils.ServiceLocator;
using Random = UnityEngine.Random;

namespace DefaultNamespace.Gameplay.World
{
    public class SpawnService : IService
    {
        readonly EventBus _eventBus;
        readonly GameConfig _gameConfig;
        readonly GameStateService _gameStateService;

        public SpawnService(GameConfig gameConfig)
        {
            _gameConfig = gameConfig;
            _eventBus = ServiceLocator.Current.Get<EventBus>();
            _gameStateService = ServiceLocator.Current.Get<GameStateService>();
        }

        public IEnumerator StartSpawningEnemy(Transform parent, Vector2 bounds)
        {
            while (true)
            {
                yield return new WaitForSeconds(_gameConfig.EnemySpawnDelay);
                var type = GetRandomEnemyType();
                var x = Random.Range(0, bounds.x);
                var y = Random.Range(0, bounds.y);
                var position = new Vector2(x, y);
                SpawnAtPosition(type, parent, position);
            }
        }
        public void SpawnAtPosition(EnemyType enemyType, Transform parent, Vector2 position)
        {
            var config = _gameConfig.EnemyConfigs.FirstOrDefault(p => p.Type == enemyType);
            if (!config) throw new Exception($"Enemy config {enemyType} not found");

            var enemyState = config.Create();
            enemyState.Position = position;
            
            _gameStateService.GameState.Enemies.Add(enemyState);
            
            var instance = GameObject.Instantiate(config.Prefab, position, Quaternion.identity, parent);
            
            var enemyView = instance.GetComponent<EnemyView>();
            var enemyController = new EnemyController(enemyState);
            var enemyViewModel = new EnemyViewModel(enemyState, enemyController);
            enemyView.Bind(enemyViewModel);
            
            _eventBus.TriggerEvent(new GameEvent(EventName.EnemySpawned,
                $"Enemy {enemyType} spawned at pos:{position}"));
        }
        public static EnemyType GetRandomEnemyType()
        {
            var values = Enum.GetValues(typeof(EnemyType));
            int randomIndex = Random.Range(1, values.Length);
            return (EnemyType)values.GetValue(randomIndex);
        }
    }
}