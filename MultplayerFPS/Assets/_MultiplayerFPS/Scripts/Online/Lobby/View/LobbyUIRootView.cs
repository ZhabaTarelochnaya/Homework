using UnityEngine;

namespace _MultiplayerFPS.Scripts.Online.Lobby.View
{
    public class LobbyUIRootView : MonoBehaviour
    {
        [SerializeField] LobbyUIView _lobbyUIView;

        public void Init(NetworkPlayer player)
        {
            var lobbyState = FindObjectOfType<LobbyState>();
            _lobbyUIView.Init(lobbyState, player);
        }
    }
}