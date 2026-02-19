using _MultiplayerFPS.Scripts.Utils.ServiceLocator;
using Mirror;

namespace _MultiplayerFPS.Scripts.Services
{
    public interface INetworkService : IService
    {
        public NetManager NetManager { get; }
        public NetDiscovery NetDiscovery { get; }
    }
}