using System;
using System.Collections.Generic;
using _MultiplayerFPS.Scripts.Online.Lobby;
using Mirror;
using UnityEngine;

public class PlayerListView : MonoBehaviour
{
    Dictionary<NetworkPlayer, PlayerCardView> _playerCards = new ();
    LobbyState _lobbyState;
    [SerializeField] PlayerCardView playerCardViewPrefab;
    [SerializeField] RectTransform _viewport;

    public void Init(LobbyState lobbyState)
    {
        _lobbyState = lobbyState;
        foreach (var player in _lobbyState.Players)
        {
            CreatePlayerCard(player);
        }
        _lobbyState.Players.OnAdd += OnAdd;
        _lobbyState.Players.OnRemove += OnRemove;
    }

    void CreatePlayerCard(NetworkPlayer player)
    {
        var playerCard = Instantiate(playerCardViewPrefab, _viewport);
        _playerCards[player] = playerCard;
        _playerCards[player].SetNickname($"{ player.Nickname}");
        _playerCards[player].SetColor(player.Color);
        _playerCards[player].SetReady(player.readyToBegin);
        player.ClientNicknameChanged += OnPlayerNicknameChanged;
        player.ClientReadyChanged += OnPlayerReadyChanged;
        player.ClientColorChanged += OnPlayerNicknameColorChanged;
    }
    void OnAdd(int index) => CreatePlayerCard(_lobbyState.Players[index]);
    void OnRemove(int index, NetworkPlayer player)
    {
        Destroy(_playerCards[player].gameObject);
        _playerCards.Remove(player);
        player.ClientNicknameChanged -= OnPlayerNicknameChanged;
        player.ClientReadyChanged -= OnPlayerReadyChanged;
        player.ClientColorChanged -= OnPlayerNicknameColorChanged;
    }
    void OnPlayerNicknameChanged(NetworkPlayer player, string newNickname)
    {
        if (!_playerCards.ContainsKey(player)) return;
        _playerCards[player].SetNickname(newNickname);
    }

    void OnPlayerReadyChanged(NetworkPlayer player, bool readyState)
    {
        _playerCards[player].SetReady(readyState);
    }
    void OnPlayerNicknameColorChanged(NetworkPlayer player, Color color)
    {
        if (!_playerCards.ContainsKey(player)) return;
        _playerCards[player].SetColor(color);
    }
}
