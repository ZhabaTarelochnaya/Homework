using System.Collections.Generic;
using _MultiplayerFPS.Scripts.Config.Pickup;
using _MultiplayerFPS.Scripts.Utils.ServiceLocator;

namespace _MultiplayerFPS.Scripts.Services
{
    public interface IPickupService : IService
    {
        public void Spawn(PickupName name);
        public void SpawnRandom();
        public bool TryPickup(uint itemNetId, uint playerNetId);
    }
}