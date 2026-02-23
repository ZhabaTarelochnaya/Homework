using System;
using TMPro;
using UnityEngine;

namespace _MultiplayerFPS.Scripts.Offline
{
    public class DiscoveredLobbyView : MonoBehaviour
    {
        [SerializeField] TMP_Text _hostPlayerName;
        [SerializeField] TMP_Text _playerCount;
        Uri _uri;
        Action<Uri> _serverButtonClicked;
        public void Init(string hostPlayerName, int playerCount, Uri uri, Action<Uri> serverButtonClicked)
        {
            _hostPlayerName.text = hostPlayerName;
            _playerCount.text = playerCount.ToString();
            _uri = uri;
            _serverButtonClicked = serverButtonClicked;
        }
        public void OnServerButtonClicked() => _serverButtonClicked?.Invoke(_uri);
    }
}