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

        public void Init(string hostPlayerName, int playerCount, Uri uri)
        {
            _hostPlayerName.text = hostPlayerName;
            _playerCount.text = playerCount.ToString();
            _uri = uri;
        }

        public void OnServerButtonClicked()
        {
            NetManager.singleton.StartClient(_uri);
        }
    }
}