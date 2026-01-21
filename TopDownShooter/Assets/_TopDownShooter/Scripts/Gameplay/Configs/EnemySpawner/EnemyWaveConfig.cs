using System.Collections.Generic;
using UnityEngine;

namespace _TopDownShooter.Scripts.Gameplay.Configs
{
    [CreateAssetMenu(fileName = "EnemyWaveConfig", menuName = "ScriptableObjects/EnemyWaveConfig")]
    public class EnemyWaveConfig : ScriptableObject
    {
        [field: SerializeField] public float Duration { get; private set; }
        [field: SerializeField] public List<EnemyInWaveCountConfig> Composition { get; private set; }
    }
}