using System;
using System.Collections;
using DefaultNamespace.Gameplay.World;
using UnityEditor;
using UnityEngine;
using Utils.EventBus;
using Utils.ServiceLocator;
using Object = UnityEngine.Object;

namespace DefaultNamespace.Gameplay.View.PickUp
{
    public class EnemySpawnerViewModel
    {
        readonly SpawnService _spawnService = ServiceLocator.Current.Get<SpawnService>();

        public IEnumerator StartSpawning(Transform transform, Vector2 bounds)
        {
            return _spawnService.StartSpawningEnemy(transform, bounds);
        }
    }
}