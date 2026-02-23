
using System;
using System.Collections.Generic;
using UnityEngine;

namespace _MultiplayerFPS.Scripts.Offline
{
    public class SelectLobbyView : MonoBehaviour, ISelectLobbyView
    {
        
        
        [SerializeField] GameObject _mainMenu;
        [SerializeField] RectTransform _content;
        [SerializeField] DiscoveredLobbyView _discoveredLobbyViewPrefab;
        
        public event Action<Uri> ServerChosen;
        public event Action ExitButtonClicked;
        public event Action RefreshButtonClicked;

        public void OnExitButtonClick() => ExitButtonClicked?.Invoke();
        public void OnRefreshServerListButtonClick() => RefreshButtonClicked?.Invoke();
        public void ClearDiscoveredLobbies()
        {
            for (int i = _content.transform.childCount - 1; i >= 0; i--)
            {
                GameObject child = _content.transform.GetChild(i).gameObject;
                Destroy(child);
            }
        }
        public void SpawnDiscoveredLobbyView(string hostPlayerName, int playerCount, Uri uri)
        {
            var discoveredLobbyView = Instantiate(_discoveredLobbyViewPrefab, _content);
            discoveredLobbyView.Init(hostPlayerName, playerCount, uri, ChooseServer);
        }
        public void Enable() => gameObject.SetActive(true);
        public void Disable() => gameObject.SetActive(false);
        void ChooseServer(Uri uri) => ServerChosen?.Invoke(uri);
    }
}