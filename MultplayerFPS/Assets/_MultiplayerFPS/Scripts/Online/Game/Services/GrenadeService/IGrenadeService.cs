using _MultiplayerFPS.Scripts.State;
using _MultiplayerFPS.Scripts.Utils.ServiceLocator;
using UnityEngine;

namespace _MultiplayerFPS.Scripts.Services.GrenadeService
{
    public interface IGrenadeService : IService
    {
        public void ThrowGrenade(PlayerState playerState, Vector3 origin, Vector3 direction);
    }
}