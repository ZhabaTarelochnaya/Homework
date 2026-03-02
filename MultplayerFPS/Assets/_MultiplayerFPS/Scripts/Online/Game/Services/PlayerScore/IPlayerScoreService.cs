using _MultiplayerFPS.Scripts.Utils.ServiceLocator;

namespace _MultiplayerFPS.Scripts.Services
{
    public interface IPlayerScoreService : IService
    {
        public void AddKill(uint playerNetId);
        public void AddDeath(uint playerNetId);
    }
}