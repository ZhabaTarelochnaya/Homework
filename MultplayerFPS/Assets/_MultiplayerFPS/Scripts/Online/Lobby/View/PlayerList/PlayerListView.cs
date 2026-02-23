using System.Collections.Generic;
using _MultiplayerFPS.Scripts.Online.Lobby;
using UnityEngine;

public class PlayerListView : MonoBehaviour, IPlayerListView
{
    Dictionary<uint, PlayerCardView> _playerCards = new ();
    [SerializeField] PlayerCardView playerCardViewPrefab;
    [SerializeField] RectTransform _viewport;
    
    public void CreatePlayerCard(uint netId, string nickName, Color color, bool ready)
    {
        var playerCard = Instantiate(playerCardViewPrefab, _viewport);
        playerCard.SetNickname(nickName);
        playerCard.SetColor(color);
        playerCard.SetReady(ready);
        _playerCards[netId] = playerCard;
    }
    public void RemovePlayerCard(uint netId)
    {
        Destroy(_playerCards[netId].gameObject);
        _playerCards.Remove(netId);
    }
    public void SetPlayerNickname(uint netId, string nickName) => _playerCards[netId].SetNickname(nickName);
    public void SetPlayerColor(uint netId, Color color) => _playerCards[netId].SetColor(color);
    public void SetPlayerReady(uint netId, bool ready) => _playerCards[netId].SetReady(ready);
    public void Enable() => gameObject.SetActive(true);
    public void Disable() => gameObject.SetActive(false);
}
