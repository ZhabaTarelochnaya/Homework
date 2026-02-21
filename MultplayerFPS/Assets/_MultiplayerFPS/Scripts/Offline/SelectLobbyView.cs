
using System.Collections.Generic;
using UnityEngine;

namespace _MultiplayerFPS.Scripts.Offline
{
    public class SelectLobbyView : MonoBehaviour
    {
        readonly Dictionary<long, DiscoveryResponse> discoveredServers = new ();
        [SerializeField] GameObject _mainMenu;
        [SerializeField] RectTransform _content;
        [SerializeField] DiscoveredLobbyView _discoveredLobbyViewPrefab;

        void Awake()
        {
            NetManager.singleton.GetComponent<NetDiscovery>().OnServerFound.AddListener(OnDiscoveredServer);
        }

        void OnDiscoveredServer(DiscoveryResponse response)
        {
            if (discoveredServers.ContainsKey(response.ServerId)) return;
            discoveredServers[response.ServerId] = response;
            var discoveredLobbyView = Instantiate(_discoveredLobbyViewPrefab, _content);
            discoveredLobbyView.Init(response.HostPlayerName, response.PlayerCount, response.Uri);
        }

        public void OnExitButtonClick()
        {
            _mainMenu.SetActive(true);
            gameObject.SetActive(false);
        }

        public void OnRefreshServerListButtonClick()
        {
            discoveredServers.Clear();
            for (int i = _content.transform.childCount - 1; i >= 0; i--)
            {
                GameObject child = _content.transform.GetChild(i).gameObject;
                Destroy(child);
            }
            NetManager.singleton.GetComponent<NetDiscovery>().StartDiscovery();
        }
    }
}