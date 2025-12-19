namespace NPCEventNotification.Scripts.Gameplay.NPC
{
    public interface IAttackerData
    {
        int Damage { get; set; }
        float AttackCooldown { get; set; }
    }
}