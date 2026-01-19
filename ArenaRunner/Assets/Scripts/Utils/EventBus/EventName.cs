namespace Utils.EventBus
{
    public enum EventName
    {
        Default,
        /// <summary>
        /// args: int currentScore
        /// </summary>
        ScoreChanged,
        ItemPicked,
        EnemySpawned,
        /// <summary>
        /// args: GameStateName name
        /// </summary>
        GameStateChanged,
        GamePaused,
        /// <summary>
        /// args: int currentHp
        /// </summary>
        PlayerDamaged,
    }
}