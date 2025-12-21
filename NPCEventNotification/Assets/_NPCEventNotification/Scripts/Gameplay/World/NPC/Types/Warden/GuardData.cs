using NPCEventNotification.Scripts.Gameplay.NPC;
using NPCEventNotification.Scripts.Gameplay.NPC.Behaviours.Types.Chase;
using NPCEventNotification.Scripts.Gameplay.NPC.Behaviours.Wander;
using UnityEngine;

namespace NPCEventNotification.Scripts.Gameplay.World.NPC.Types.Warden
{
    public class GuardData : IAgentData, IWandererData, IHealthData, IAttackerData
    {
        public Vector2 Destination { get; set; }
        public float SetDestinationFrequency { get; set; }
        public float WanderDistance { get; set; }
        public float WanderStopTime { get; set; }
        public int Damage { get; set; }
        public float AttackCooldown { get; set; }
        public int MaxHealth { get; set; }
        public int CurrentHealth { get; set; }
        public float Speed { get; set; }
        
    }
}