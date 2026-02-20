namespace _MultiplayerFPS.Scripts.Utils.EventBus
{
    public enum EventName
    {
        Default,
        /// <summary>
        /// args: NetworkPlayer networkPlayer.
        /// </summary>
        OnPlayerStartClient,
        /// <summary>
        /// args: NetworkPlayer networkPlayer.
        /// </summary>
        OnClientExitRoom,
        /// <summary>
        /// args: NetworkPlayer networkPlayer, bool newReady.
        /// </summary>
        ReadyStateChanged,
        /// <summary>
        /// args: NetworkPlayer networkPlayer, string newNickname.
        /// </summary>
        OnPlayerNicknameChanged
    }
}