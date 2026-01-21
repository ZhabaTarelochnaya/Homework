using UnityEngine;

namespace _TopDownShooter.Scripts.Gameplay.Configs.Enemies
{
    [CreateAssetMenu(fileName = "EnemyConfig", menuName = "ScriptableObjects/EnemyConfig")]
    public class EnemyConfig : ScriptableObject
    {
        [field: SerializeField] public EnemyName Name { get; private set; }
        [field: SerializeField] public int Damage { get; private set; }
        [field: SerializeField] public int MaxHealth { get; private set; }
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public GameObject Prefab { get; private set; }
    }
}