using System;
using _MultiplayerFPS.Scripts.Services;
using _MultiplayerFPS.Scripts.Services.SceneManager;
using _MultiplayerFPS.Scripts.Utils.ServiceLocator;
using Mirror;
using TMPro;
using UnityEngine;

namespace _MultiplayerFPS.Scripts.Offline
{
    public class DiscoveredLobbyView : MonoBehaviour
    {
        INetworkService _networkService;
        [SerializeField] TMP_Text _hostPlayerName;
        [SerializeField] TMP_Text _playerCount;
        Uri _uri;

        public void Init(string hostPlayerName, int playerCount, Uri uri)
        {
            _hostPlayerName.text = hostPlayerName;
            _playerCount.text = playerCount.ToString();
            _networkService = ServiceLocator.Current.Get<INetworkService>();
            _uri = uri;
        }

        public void OnServerButtonClicked()
        {
            _networkService.NetManager.StartClient(_uri);
        }
    }
}