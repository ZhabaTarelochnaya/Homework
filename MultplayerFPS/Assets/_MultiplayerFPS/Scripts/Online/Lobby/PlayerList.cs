using System.Collections;
using System.Collections.Generic;
using _MultiplayerFPS.Scripts.Online.Lobby;
using _MultiplayerFPS.Scripts.Services;
using _MultiplayerFPS.Scripts.Utils.EventBus;
using _MultiplayerFPS.Scripts.Utils.ServiceLocator;
using Mirror;
using UnityEngine;

public class PlayerList : MonoBehaviour
{
    Dictionary<NetworkPlayer, PlayerCard> _players = new ();
    EventBus _eventBus;
    [SerializeField] PlayerCard _playerCardPrefab;
    [SerializeField] RectTransform _viewport;

    public void Awake()
    {
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        _eventBus.GameEventFired += EventBusOnGameEventFired;
    }
    void OnPlayerStartClient(NetworkPlayer player)
    {
        var playerCard = Instantiate(_playerCardPrefab, _viewport);
        playerCard.SetNickname($"{player.Nickname}");
        playerCard.SetColor(player.Color);
        
        _players[player] = playerCard;
    }
    void OnPlayerExitClient(NetworkPlayer player)
    {
        Destroy(_players[player].gameObject);
        _players.Remove(player);
    }
    void OnPlayerNicknameChanged(NetworkPlayer player, string newNickname) => _players[player].SetNickname(newNickname);
    void OnPlayerReadyChanged(NetworkPlayer player, bool readyState) => _players[player].SetReady(readyState);
    void EventBusOnGameEventFired(GameEvent e)
    {
        switch (e.Name)
        {
            case EventName.OnPlayerStartClient:
                OnPlayerStartClient((NetworkPlayer)e.Args[0]);
                break;
            case EventName.OnClientExitRoom:
                OnPlayerExitClient((NetworkPlayer)e.Args[0]);
                break;
            case EventName.ReadyStateChanged:
                OnPlayerReadyChanged((NetworkPlayer)e.Args[0], (bool)e.Args[1]);
                break;
            case EventName.OnPlayerNicknameChanged:
                OnPlayerNicknameChanged((NetworkPlayer)e.Args[0], (string)e.Args[1]);
                break;
                
        }
    }

    void OnDestroy()
    {
        _eventBus.GameEventFired -= EventBusOnGameEventFired;
    }
}
