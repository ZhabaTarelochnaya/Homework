using _MultiplayerFPS.Scripts.Utils;
using Mirror;
using UnityEngine;

namespace _MultiplayerFPS.Scripts
{
    public class HUDPresenter : IPresenter
    {
        readonly HUDView _view;

        public HUDPresenter(HUDView view)
        {
            _view = view;
            Enable();
        }
        public void Enable()
        {
            _view.UpdatingPing += ViewOnUpdatingPing;
            _view.Enable();
        }

        void ViewOnUpdatingPing()
        {
            int ping = Mathf.RoundToInt((float)(NetworkTime.rtt * 1000));
            _view.SetPing(ping);
            _view.SetPlayerCount(NetworkServer.connections.Count);
        }

        public void Disable()
        {
            _view.Disable();
            _view.UpdatingPing -= ViewOnUpdatingPing;
        }
    }
}