using System;
using _MultiplayerFPS.Scripts.Utils.ServiceLocator;

namespace _MultiplayerFPS.Scripts.Services.MatchTime
{
    public interface IMatchTimeService : IService
    {
        public event Action TimeOut;
        public void StartTimer();
    }
}