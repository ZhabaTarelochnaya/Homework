using _MultiplayerFPS.Scripts.Utils.ServiceLocator;
using Mirror;

namespace _MultiplayerFPS.Scripts.Services.SceneManager
{
    public interface ISceneManagerService : IService
    {
        public void LoadScene(string sceneName);
        public void LoadServerScene(string sceneName);
        public void LoadLobbyAndHost();
    }
}