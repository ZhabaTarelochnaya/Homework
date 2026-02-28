using Mirror;

namespace _MultiplayerFPS.Scripts.State
{
    public class GameState : NetworkBehaviour
    {
        public SyncDictionary<uint, PlayerState> PlayerStates { get; } = new ();
    }
}