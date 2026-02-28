using _MultiplayerFPS.Scripts.Utils.ServiceLocator;
using UnityEngine;

namespace _MultiplayerFPS.Scripts.Services.Respawn
{
    public interface IRespawnService : IService
    {
        public void Respawn(CharacterController player);
    }
}