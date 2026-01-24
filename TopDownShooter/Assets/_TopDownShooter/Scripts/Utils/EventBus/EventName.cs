namespace _TopDownShooter.Scripts.Utils.EventBus
{
    public enum EventName
    {
        Default,
        /// <summary>
        /// args: int damage, HurtBox hurtBox.
        /// </summary>
        PlayerHurt,
        /// <summary>
        /// args: float reloadTime.
        /// </summary>
        ReloadStarted,
        ReloadStopped,
        EnemyKilled,
        WeaponSwitched,
        /// <summary>
        /// args: GameStateName newState
        /// </summary>
        GameStateChanged,
        Won,
        Lost
    }
}