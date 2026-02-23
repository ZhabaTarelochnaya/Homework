using System;
using _MultiplayerFPS.Scripts.Utils;

namespace _MultiplayerFPS.Scripts.Offline
{
    public interface ISelectLobbyView : IEnableView
    {
        public event Action<Uri> ServerChosen;
        public event Action ExitButtonClicked;
        public event Action RefreshButtonClicked;

        public void SpawnDiscoveredLobbyView(string hostPlayerName, int playerCount, Uri uri);
        public void ClearDiscoveredLobbies();
        
    }
}