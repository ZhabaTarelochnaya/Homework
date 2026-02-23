using _MultiplayerFPS.Scripts.Utils;
using UnityEngine;

namespace _MultiplayerFPS.Scripts.Online.Lobby
{
    public class PlayerListPresenter : IPresenter
    {
        readonly LobbyState _lobbyState;
        readonly IPlayerListView _playerListView;

        public PlayerListPresenter(LobbyState lobbyState, IPlayerListView playerListView)
        {
            _lobbyState = lobbyState;
            _playerListView = playerListView;
            Enable();
        }
        public void Enable()
        {
            _playerListView.Enable();
            foreach (var player in _lobbyState.Players)
            {
                _playerListView.CreatePlayerCard(player.netId, player.Nickname, 
                    player.Color, player.readyToBegin);
                player.ClientNicknameChanged += OnPlayerNicknameChanged;
                player.ClientReadyChanged += OnPlayerReadyChanged;
                player.ClientColorChanged += OnPlayerNicknameColorChanged;
            }
            _lobbyState.Players.OnAdd += OnAdd;
            _lobbyState.Players.OnRemove += OnRemove;
        }
        public void Disable()
        {
            _lobbyState.Players.OnAdd -= OnAdd;
            _lobbyState.Players.OnRemove -= OnRemove;
            foreach (var player in _lobbyState.Players)
            {
                _playerListView.RemovePlayerCard(player.netId);
                player.ClientNicknameChanged -= OnPlayerNicknameChanged;
                player.ClientReadyChanged -= OnPlayerReadyChanged;
                player.ClientColorChanged -= OnPlayerNicknameColorChanged;
            }
            _playerListView.Disable();
        }
        void OnAdd(int index)
        {
            var player = _lobbyState.Players[index];
            _playerListView.CreatePlayerCard(player.netId, player.Nickname, 
                player.Color, player.readyToBegin);
            player.ClientNicknameChanged += OnPlayerNicknameChanged;
            player.ClientReadyChanged += OnPlayerReadyChanged;
            player.ClientColorChanged += OnPlayerNicknameColorChanged;
        }
        void OnRemove(int index, NetworkPlayer player)
        {
            _playerListView.RemovePlayerCard(player.netId);
            player.ClientNicknameChanged -= OnPlayerNicknameChanged;
            player.ClientReadyChanged -= OnPlayerReadyChanged;
            player.ClientColorChanged -= OnPlayerNicknameColorChanged;
        }
        void OnPlayerNicknameChanged(NetworkPlayer player, string newNickname)
        {
            _playerListView.SetPlayerNickname(player.netId, newNickname);
        }
        void OnPlayerReadyChanged(NetworkPlayer player, bool readyState)
        {
            _playerListView.SetPlayerReady(player.netId, readyState);
        }
        void OnPlayerNicknameColorChanged(NetworkPlayer player, Color color)
        {
            _playerListView.SetPlayerColor(player.netId, color);
        }
    }
}