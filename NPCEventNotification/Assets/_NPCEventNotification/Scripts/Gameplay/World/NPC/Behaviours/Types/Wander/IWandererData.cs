namespace NPCEventNotification.Scripts.Gameplay.NPC.Behaviours.Wander
{
    public interface IWandererData
    {
        float WanderDistance { get; set; }
        float WanderStopTime { get; set; }
    }
}