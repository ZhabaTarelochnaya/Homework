using _TopDownShooter.Scripts.Gameplay.Configs.Enemies;
using UnityEngine;

namespace _TopDownShooter.Scripts.Gameplay.Configs
{
    [CreateAssetMenu(fileName = "EnemyInWaveCountConfig", menuName = "ScriptableObjects/EnemyInWaveCountConfig")]
    public class EnemyInWaveCountConfig : ScriptableObject
    {
        [field: SerializeField] public EnemyName Name { get; private set; }
        [field: SerializeField] public int Count { get; private set; }
    }
}