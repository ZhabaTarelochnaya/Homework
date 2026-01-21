using System.Collections;
using System.Collections.Generic;
using _TopDownShooter.Scripts.Gameplay.Configs;
using _TopDownShooter.Scripts.Gameplay.Configs.Enemies;
using _TopDownShooter.Scripts.Gameplay.Services;
using _TopDownShooter.Scripts.Utils.Extensions;
using _TopDownShooter.Scripts.Utils.ServiceLocator;
using UnityEngine;
using UnityEngine.AI;

namespace _TopDownShooter.Scripts.View
{
    public class EnemySpawnerController
    {
        int _currentWave = 0;
        readonly Transform _enemiesParent;
        readonly Transform _player;
        readonly Transform[] _spawnPoints;
        readonly ConfigProviderService _configProvider;
        readonly EnemySpawnerConfig _spawnerConfig;
        readonly List<EnemyName> _enemiesToSpawn = new ();

        public EnemySpawnerController(Transform spawnPoints, Transform enemiesParent, Transform player)
        {
            _enemiesParent = enemiesParent;
            _player = player;
            _spawnPoints = new Transform[spawnPoints.childCount];
            for (int i = 0; i < _spawnPoints.Length; i++)
            {
                _spawnPoints[i] = spawnPoints.GetChild(i);
            }
            _configProvider = ServiceLocator.Current.Get<ConfigProviderService>();
            _spawnerConfig = _configProvider.GetEnemySpawnerConfig();
        }
        public IEnumerator Spawn()
        {
            while (_currentWave < _spawnerConfig.WaveConfigs.Count)
            {
                var waveConfig = _spawnerConfig.WaveConfigs[_currentWave];
                CollectEnemiesFromWave(waveConfig);

                yield return WaitForSpawn();
                
                yield return new WaitForSeconds(waveConfig.Duration);
                _currentWave += 1;
            }
        }
        void CollectEnemiesFromWave(EnemyWaveConfig waveConfig)
        {
            foreach (var enemyInWaveConfig in waveConfig.Composition)
            {
                for (int i = 0; i < enemyInWaveConfig.Count; i++)
                {
                    _enemiesToSpawn.Add(enemyInWaveConfig.Name);
                }
            }
        }
        IEnumerator WaitForSpawn()
        {
            while (_enemiesToSpawn.Count > 0)
            {
                foreach (var spawnPoint in _spawnPoints)
                {
                    if (_enemiesToSpawn.Count == 0) break;
                    InstantiateEnemy(_enemiesToSpawn.TakeRandom(), spawnPoint);
                }
                yield return new WaitForSeconds(_spawnerConfig.SpawnDelay);
            }
        }
        void InstantiateEnemy(EnemyName name, Transform spawnPoint)
        {
            var config = _configProvider.GetEnemyConfig(name);
            var instance = Object.Instantiate(config.Prefab, 
                spawnPoint.position, Quaternion.identity, _enemiesParent);
            var view = instance.GetComponent<EnemyView>();
            var controller = new EnemyController(view.Agent, view.HurtBox, view.HitBox, _player, config);
            view.Bind(controller);
            PlaceOnMesh(view.Agent);
        }

        void PlaceOnMesh(NavMeshAgent agent)
        {
            NavMesh.SamplePosition(agent.transform.position, 
                out NavMeshHit hit, 10.0f, NavMesh.AllAreas);
            agent.Warp(hit.position);
        }
    }
}