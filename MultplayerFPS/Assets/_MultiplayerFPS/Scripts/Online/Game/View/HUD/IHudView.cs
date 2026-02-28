using System;
using _MultiplayerFPS.Scripts.Utils;

namespace _MultiplayerFPS.Scripts
{
    public interface IHudView : IView
    {
        public event Action UpdatingPing;
        public void SetPingUpdateInterval(float pingUpdateInterval);
        public void SetPing(int ping);
        public void SetPlayerCount(int playerCount);
        public void SetAmmo(int current, int max);
        public void SetHealth(int health);
    }
}