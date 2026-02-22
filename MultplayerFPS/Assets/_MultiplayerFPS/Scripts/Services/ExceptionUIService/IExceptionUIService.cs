using _MultiplayerFPS.Scripts.Utils.ServiceLocator;

namespace _MultiplayerFPS.Scripts.Utils.ExceptionPopUp
{
    public interface IExceptionUIService : IService
    {
        public void ShowError(string message);
    }
}