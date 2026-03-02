using System.Collections.Generic;
using _MultiplayerFPS.Scripts.Online.Lobby;
using UnityEngine;

public class PlayerListView : MonoBehaviour, IPlayerListView
{
    [SerializeField] PlayerCardView[] _playerCards;

    public void UpdateData(PlayerCardData[] playersData)
    {
        for (int i = 0; i < _playerCards.Length; i++)
        {
            if (playersData[i] == null)
            {
                _playerCards[i].Disable();
                continue;
            }
            _playerCards[i].Enable();
            _playerCards[i].SetNickname(playersData[i].Nickname);
            _playerCards[i].SetColor(playersData[i].Color);
            _playerCards[i].SetReady(playersData[i].IsReady);
        }
    }

    public void Enable() => gameObject.SetActive(true);
    public void Disable() => gameObject.SetActive(false);
}
