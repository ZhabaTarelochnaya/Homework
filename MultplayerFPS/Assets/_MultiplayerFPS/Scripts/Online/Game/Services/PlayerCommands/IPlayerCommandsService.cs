using _MultiplayerFPS.Scripts.Utils.ServiceLocator;

namespace _MultiplayerFPS.Scripts.Services.ServerCommands
{
    public interface IPlayerCommandsService : IService
    {
        public void ReturnToLobby();
    }
}