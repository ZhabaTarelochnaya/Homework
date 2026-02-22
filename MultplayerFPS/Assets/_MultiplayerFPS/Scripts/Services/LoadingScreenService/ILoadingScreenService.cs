using _MultiplayerFPS.Scripts.Utils.ServiceLocator;

namespace _MultiplayerFPS.Scripts.Utils.LoadingScreenService
{
    public interface ILoadingScreenService : IService
    {
        public void Show();
        public void Hide();
    }
}