using _MultiplayerFPS.Scripts.Utils.ServiceLocator;
using Mirror;

namespace _MultiplayerFPS.Scripts.Services
{
    public interface INetworkManagerService : IService
    {
        NetworkManager NetworkManager { get; }
    }
}