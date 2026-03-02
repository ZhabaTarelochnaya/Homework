using _MultiplayerFPS.Scripts.Utils;
using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _MultiplayerFPS.Scripts.Online.Lobby
{
    public class PlayerCardView : MonoBehaviour, IView
    {
        [SerializeField] TMP_Text _nickname;
        [SerializeField] TMP_Text _playerStatus;
        [SerializeField] CanvasGroup _group;
        
        public void SetNickname(string newNickname) => _nickname.text = newNickname;
        public void SetReady(bool isReady) => _playerStatus.text = isReady ? "Ready" : "Unready";
        public void SetColor(Color newColor) => _nickname.color = newColor;
        public void Enable()
        {
            _group.alpha = 1;
        }
        public void Disable()
        {
            _group.alpha = 0;
        }
    }
}