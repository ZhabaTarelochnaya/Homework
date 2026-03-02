using System;
using System.Linq;
using _MultiplayerFPS.Scripts.Utils;
using Mirror;
using UnityEngine;

namespace _MultiplayerFPS.Scripts.Online.Lobby
{
    public class PlayerListPresenter : IPresenter
    {
        const int MaxPlayers = 8;
        readonly LobbyState _lobbyState;
        readonly IPlayerListView _playerListView;

        public PlayerListPresenter(LobbyState lobbyState, IPlayerListView playerListView)
        {
            _lobbyState = lobbyState;
            _playerListView = playerListView;
        }
        public void Enable()
        {
            _playerListView.Enable();
            Refresh();
            foreach (var player in _lobbyState.Players)
            {
                player.ClientNicknameChanged += OnPlayerNicknameChanged;
                player.ClientReadyChanged += OnPlayerReadyChanged;
                player.ClientColorChanged += OnPlayerNicknameColorChanged;
            }
            _lobbyState.Players.OnAdd += OnAdd;
            _lobbyState.Players.OnRemove += OnRemove;
        }
        public void Disable()
        {
            _playerListView.Disable();
            foreach (var player in _lobbyState.Players)
            {
                player.ClientNicknameChanged -= OnPlayerNicknameChanged;
                player.ClientReadyChanged -= OnPlayerReadyChanged;
                player.ClientColorChanged -= OnPlayerNicknameColorChanged;
            }
            _lobbyState.Players.OnAdd -= OnAdd;
            _lobbyState.Players.OnRemove -= OnRemove;
        }
        void Refresh()
        {
            var data = new PlayerCardData?[MaxPlayers];
            int index = 0;
            foreach (var p in _lobbyState.Players)
            {
                if (index >= MaxPlayers) break;

                var nickname = p.IsHost ? $"{p.Nickname} (Host)" : p.Nickname;
                data[index++] = new PlayerCardData(nickname, p.Color, p.readyToBegin);
            }
            _playerListView.UpdateData(data);
        }
        void OnAdd(int index)
        {
            Refresh();
            var player = _lobbyState.Players[index];
            player.ClientNicknameChanged += OnPlayerNicknameChanged;
            player.ClientReadyChanged += OnPlayerReadyChanged;
            player.ClientColorChanged += OnPlayerNicknameColorChanged;
        }
        void OnRemove(int index, NetworkPlayer player)
        {
            player.ClientNicknameChanged -= OnPlayerNicknameChanged;
            player.ClientReadyChanged -= OnPlayerReadyChanged;
            player.ClientColorChanged -= OnPlayerNicknameColorChanged;
            Refresh();
        }
        void OnPlayerNicknameChanged(NetworkPlayer player, string newNickname)
        {
            Refresh();
        }
        void OnPlayerReadyChanged(NetworkPlayer player, bool readyState)
        {
            Refresh();
        }
        void OnPlayerNicknameColorChanged(NetworkPlayer player, Color color)
        {
            Refresh();
        }
    }
}