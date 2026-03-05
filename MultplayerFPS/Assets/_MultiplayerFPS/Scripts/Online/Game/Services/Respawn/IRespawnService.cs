using System;
using _MultiplayerFPS.Scripts.Utils.ServiceLocator;
using UnityEngine;

namespace _MultiplayerFPS.Scripts.Services.Respawn
{
    public interface IRespawnService : IService
    {
        public event Action Respawned;
        public void Respawn(CharacterController player);
    }
}