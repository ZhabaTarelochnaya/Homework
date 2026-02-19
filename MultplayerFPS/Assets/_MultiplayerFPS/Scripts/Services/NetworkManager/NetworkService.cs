using Mirror;

namespace _MultiplayerFPS.Scripts.Services
{
    public class NetworkService : INetworkService
    {
        public NetManager NetManager { get; }
        public NetDiscovery NetDiscovery { get; }
        public NetworkService(NetManager netManager)
        {
            NetManager = netManager;
            NetDiscovery = netManager.GetComponent<NetDiscovery>();
        }
    }
}