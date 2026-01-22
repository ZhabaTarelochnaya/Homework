namespace _TopDownShooter.Scripts.Utils.EventBus
{
    public enum EventName
    {
        Default,
        /// <summary>
        /// args: int currentHP
        /// </summary>
        PlayerHurt,
        EnemyKilled
    }
}