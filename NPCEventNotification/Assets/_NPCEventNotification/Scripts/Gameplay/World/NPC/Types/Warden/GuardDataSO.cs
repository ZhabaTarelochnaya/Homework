using UnityEngine;

namespace NPCEventNotification.Scripts.Gameplay.World.NPC.Types.Warden
{
    [CreateAssetMenu(fileName = "GuardData", menuName = "ScriptableObjects/GuardData")]
    public class GuardDataSO : ScriptableObject
    {   
        [field: SerializeField] public float SetDestinationFrequency { get; private set; }
        [field: SerializeField] public float WanderDistance { get; private set; }
        [field: SerializeField] public float WanderStopTime { get; private set; }
        [field: SerializeField] public int Damage { get; private set; }
        [field: SerializeField] public int AttackCooldown { get; private set; }
        [field: SerializeField] public int MaxHealth { get; private set; }
        [field: SerializeField] public float Speed { get; private set; }
        
        public void FillData(GuardData guardData)
        {
            guardData.SetDestinationFrequency = SetDestinationFrequency;
            guardData.WanderDistance = WanderDistance;
            guardData.WanderStopTime = WanderStopTime;
            guardData.Damage = Damage;
            guardData.MaxHealth = MaxHealth;
            guardData.Speed = Speed;
            guardData.AttackCooldown = AttackCooldown;
        }
    }
}