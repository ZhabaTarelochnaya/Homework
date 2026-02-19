using System.Collections.Generic;
using _MultiplayerFPS.Scripts.Services;
using _MultiplayerFPS.Scripts.Services.SceneManager;
using _MultiplayerFPS.Scripts.Utils.ServiceLocator;
using UnityEngine;
using UnityEngine.UI;

namespace _MultiplayerFPS.Scripts.Offline
{
    public class SelectLobbyView : MonoBehaviour
    {
        INetworkService _networkService;
        ISceneManagerService _sceneManagerService;
        readonly Dictionary<long, DiscoveryResponse> discoveredServers = new ();
        [SerializeField] GameObject _mainMenu;
        [SerializeField] RectTransform _content;
        [SerializeField] DiscoveredLobbyView _discoveredLobbyViewPrefab;

        void Awake()
        {
            _networkService = ServiceLocator.Current.Get<INetworkService>();
            _sceneManagerService = ServiceLocator.Current.Get<ISceneManagerService>();
            _networkService.NetDiscovery.OnServerFound.AddListener(OnDiscoveredServer);
        }

        void OnDiscoveredServer(DiscoveryResponse response)
        {
            if (discoveredServers.ContainsKey(response.ServerId)) return;
            Debug.Log(response.ServerId);
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
            _networkService.NetDiscovery.StartDiscovery();
        }
    }
}