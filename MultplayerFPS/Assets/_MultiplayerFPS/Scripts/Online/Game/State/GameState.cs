using _MultiplayerFPS.Scripts.Components;
using Mirror;

namespace _MultiplayerFPS.Scripts.State
{
    public class GameState : NetworkBehaviour
    {
        public readonly SyncDictionary<uint, PlayerState> PlayerStates = new ();
        public readonly SyncDictionary<uint, Pickup> ActivePickups = new();
    }
}