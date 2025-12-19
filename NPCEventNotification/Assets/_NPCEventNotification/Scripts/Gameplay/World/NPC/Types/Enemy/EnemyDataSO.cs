using UnityEngine;

namespace NPCEventNotification.Scripts.Gameplay.NPC
{
    [CreateAssetMenu(fileName = "ScriptableObjects", menuName = "ScriptableObjects/EnemyDataSO")]
    public class EnemyDataSO : ScriptableObject
    {
        [field: SerializeField] public float SetDestinationFrequency { get; private set; }
        [field: SerializeField] public float WanderDistance { get; private set; }
        [field: SerializeField] public float WanderStopTime { get; private set; }
        [field: SerializeField] public int Damage { get; private set; }
        [field: SerializeField] public int AttackCooldown { get; private set; }
        [field: SerializeField] public int MaxHealth { get; private set; }
        [field: SerializeField] public float Speed { get; private set; }

        public void FillData(EnemyData enemyData)
        {
            enemyData.SetDestinationFrequency = SetDestinationFrequency;
            enemyData.WanderDistance = WanderDistance;
            enemyData.WanderStopTime = WanderStopTime;
            enemyData.Damage = Damage;
            enemyData.MaxHealth = MaxHealth;
            enemyData.Speed = Speed;
            enemyData.AttackCooldown = AttackCooldown;
        }
    }
}