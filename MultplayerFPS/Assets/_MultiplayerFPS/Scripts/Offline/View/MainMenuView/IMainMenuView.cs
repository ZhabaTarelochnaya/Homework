using System;
using _MultiplayerFPS.Scripts.Utils;

namespace _MultiplayerFPS.Scripts.Offline.View.MainMenuView
{
    public interface IMainMenuView : IEnableView
    {
        public event Action ExitButtonClicked;
        public event Action HostButtonClicked;
        public event Action DiscoverButtonClicked;
        public event Action ClientButtonClicked;
        public event Action<string> IpInputFieldEndEdit;
    }
}