using System;
using System.Collections.Generic;
using _MultiplayerFPS.Scripts.Offline.View.MainMenuView;
using _MultiplayerFPS.Scripts.Utils;

namespace _MultiplayerFPS.Scripts.Offline
{
    public class SelectLobbyPresenter : IPresenter
    {
        readonly ISelectLobbyView _selectLobbyView;
        readonly Dictionary<long, DiscoveryResponse> discoveredServers = new ();
        readonly NetDiscovery _netDiscovery;
        
        public event Action ExitClicked;
        
        public SelectLobbyPresenter(ISelectLobbyView selectLobbyView)
        {
            _selectLobbyView = selectLobbyView;
            _netDiscovery = NetManager.singleton.GetComponent<NetDiscovery>();
            Enable();
        }
        void SelectLobbyViewOnServerChosen(Uri uri) => NetManager.singleton.StartClient(uri);
        void SelectLobbyViewOnRefreshButtonClicked()
        {
            _selectLobbyView.ClearDiscoveredLobbies();
            discoveredServers.Clear();
            _netDiscovery.StartDiscovery();
        }
        void SelectLobbyViewOnExitButtonClicked() => ExitClicked?.Invoke();
        void OnServerDiscovered(DiscoveryResponse response)
        {
            if (discoveredServers.ContainsKey(response.ServerId)) return;
            discoveredServers[response.ServerId] = response;
            _selectLobbyView.SpawnDiscoveredLobbyView(response.HostPlayerName, response.PlayerCount, response.Uri);
        }
        public void Enable()
        {
            _netDiscovery.OnServerFound.AddListener(OnServerDiscovered);
            _selectLobbyView.ExitButtonClicked += SelectLobbyViewOnExitButtonClicked;
            _selectLobbyView.RefreshButtonClicked += SelectLobbyViewOnRefreshButtonClicked;
            _selectLobbyView.ServerChosen += SelectLobbyViewOnServerChosen;
            
            _selectLobbyView.Enable();
            NetManager.singleton.GetComponent<NetDiscovery>().StartDiscovery(); 
        }
        public void Disable()
        {
            _selectLobbyView.ClearDiscoveredLobbies();
            _selectLobbyView.Disable();
            
            _netDiscovery.OnServerFound.RemoveListener(OnServerDiscovered);
            _selectLobbyView.ExitButtonClicked -= SelectLobbyViewOnExitButtonClicked;
            _selectLobbyView.RefreshButtonClicked -= SelectLobbyViewOnRefreshButtonClicked;
            _selectLobbyView.ServerChosen -= SelectLobbyViewOnServerChosen;
        }
    }
}