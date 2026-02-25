using _MultiplayerFPS.Scripts.Utils.ServiceLocator;

namespace _MultiplayerFPS.Scripts.Services.LoggerService
{
    public interface ILoggerService : IService
    {
        public void Log(string message);
    }
}