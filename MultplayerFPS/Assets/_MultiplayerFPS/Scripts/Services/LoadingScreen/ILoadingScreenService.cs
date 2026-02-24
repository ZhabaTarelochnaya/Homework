using _MultiplayerFPS.Scripts.Utils.ServiceLocator;

namespace _MultiplayerFPS.Scripts.Utils.LoadingScreen
{
    public interface ILoadingScreenService : IService
    {
        public void Show();
        public void Hide();
    }
}