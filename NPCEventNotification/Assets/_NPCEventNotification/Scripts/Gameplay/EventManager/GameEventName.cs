namespace NPCEventNotification.Scripts.Gameplay
{
    public enum GameEventName
    {
        Default,
        Day,
        Night,
        Evening,
        Morning,
        /// <summary>
        /// args: Transform enemy
        /// </summary>
        EnemyDetected,
        EnemyKilled,
        AllEnemiesKilled,
    }
}