using _MultiplayerFPS.Scripts.Online.Lobby.View;
using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _MultiplayerFPS.Scripts.Online.Lobby
{
    public class LobbyUIView : MonoBehaviour
    {
        LobbyState _lobbyState;
        [SerializeField] PlayerListView _playerListView;
        [SerializeField] ColorPickerView _colorPanelView;
        [SerializeField] Button _startGameButton;
        [SerializeField] TMP_Text _readyButtonText;

        public void Init(LobbyState lobbyState, NetworkPlayer networkPlayer)
        {
            _lobbyState = lobbyState;
            _playerListView.Init(lobbyState);
            _colorPanelView.Init(networkPlayer);
            _startGameButton.gameObject.SetActive(false);
            if (NetworkServer.active)
            {
                _lobbyState.AllPlayersReadyChanged += OnAllPlayersReadyChanged;
            }
        }
        public void OnReadyButtonPressed()
        {
            var networkPlayer = NetworkClient.localPlayer.GetComponent<NetworkPlayer>();
            _readyButtonText.text = networkPlayer.readyToBegin ? "Ready" : "Unready";
            networkPlayer.CmdChangeReadyState(!networkPlayer.readyToBegin);
        }
        public void OnNicknameInputFieldEndEdit(string newNickname)
        {
            var networkPlayer = NetworkClient.localPlayer.GetComponent<NetworkPlayer>();
            networkPlayer.CmdSetNickname(newNickname);
        }
        public void OnStartGameButtonPressed()
        {
            _lobbyState.AllPlayersReadyChanged -= OnAllPlayersReadyChanged;
            NetManager.singleton.CmdStartGame();
        }

        public void OnStopButtonPressed()
        {
            if (NetworkServer.active && NetworkClient.isConnected)
            {
                NetManager.singleton.StopHost(); 
            }
            else if (NetworkClient.isConnected)
            {
                NetManager.singleton.StopClient();
            }
        }
        void OnAllPlayersReadyChanged(bool allPlayersReady)
        {
            _startGameButton.gameObject.SetActive(allPlayersReady);
        }
        
    }
}