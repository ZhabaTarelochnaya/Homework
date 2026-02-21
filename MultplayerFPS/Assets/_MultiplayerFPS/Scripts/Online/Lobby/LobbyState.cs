using System;
using Mirror;
using UnityEngine;

namespace _MultiplayerFPS.Scripts.Online.Lobby
{
    public class LobbyState : NetworkBehaviour
    {
        public readonly SyncList<NetworkPlayer> Players = new();

        [SyncVar(hook = nameof(OnAllPlayersReady)), HideInInspector]
        public bool AllPlayersReady;

        public event Action<bool> AllPlayersReadyChanged;
        void OnAllPlayersReady(bool oldValue, bool newValue) => AllPlayersReadyChanged?.Invoke(newValue);
    }
}