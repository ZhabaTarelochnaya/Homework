using System.Collections.Generic;
using UnityEngine;

namespace _TopDownShooter.Scripts.Gameplay.Configs
{
    [CreateAssetMenu(fileName = "EnemySpawnerConfig", menuName = "ScriptableObjects/EnemySpawnerConfig")]
    public class EnemySpawnerConfig : ScriptableObject
    {
        [field: SerializeField] public float SpawnDelay { get; private set; }
        [field: SerializeField] public List<EnemyWaveConfig> WaveConfigs { get; private set; }
    }
}