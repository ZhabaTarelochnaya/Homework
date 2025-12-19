using UnityEngine;

namespace NPCEventNotification.Scripts.Gameplay.NPC.Behaviours.Types.Chase
{
    public interface IAgentData
    {
        public Vector2 Destination { get; }
        public float SetDestinationFrequency { get; }
    }
}