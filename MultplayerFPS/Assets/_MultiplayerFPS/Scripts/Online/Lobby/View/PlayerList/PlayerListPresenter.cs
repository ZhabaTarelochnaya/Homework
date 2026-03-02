using System;
using System.Linq;
using _MultiplayerFPS.Scripts.Utils;
using Mirror;
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
            foreach (var player in _lobbyState.Players)
            {
                player.ClientNicknameChanged -= OnPlayerNicknameChanged;
                player.ClientReadyChanged -= OnPlayerReadyChanged;
                player.ClientColorChanged -= OnPlayerNicknameColorChanged;
            }
            _lobbyState.Players.OnAdd -= OnAdd;
            _lobbyState.Players.OnRemove -= OnRemove;
            _playerListView.Disable();
        }
        void Refresh()
        {
            var data = _lobbyState.Players
                .Select(p =>
                {
                    var nickname = p.IsHost ? $"{p.Nickname} (Host)" : p.Nickname;
                    return new PlayerCardData(nickname, p.Color, p.readyToBegin);
                })
                .ToArray();

            Array.Resize(ref data, 8);
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
            Refresh();
            player.ClientNicknameChanged -= OnPlayerNicknameChanged;
            player.ClientReadyChanged -= OnPlayerReadyChanged;
            player.ClientColorChanged -= OnPlayerNicknameColorChanged;
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