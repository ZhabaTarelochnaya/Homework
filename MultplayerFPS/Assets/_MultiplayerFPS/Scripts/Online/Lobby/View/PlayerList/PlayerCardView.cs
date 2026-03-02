using Mirror;
using TMPro;
using UnityEngine;

namespace _MultiplayerFPS.Scripts.Online.Lobby
{
    public class PlayerCardView : MonoBehaviour
    {
        [SerializeField] TMP_Text _nickname;
        [SerializeField] TMP_Text _playerStatus;
        
        public void SetNickname(string newNickname) => _nickname.text = newNickname;
        public void SetReady(bool isReady) => _playerStatus.text = isReady ? "Ready" : "Unready";
        public void SetColor(Color newColor) => _nickname.color = newColor;
    }
}