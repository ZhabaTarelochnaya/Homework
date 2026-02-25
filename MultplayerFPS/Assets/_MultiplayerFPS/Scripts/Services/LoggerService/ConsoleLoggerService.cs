using UnityEngine;

namespace _MultiplayerFPS.Scripts.Services.LoggerService
{
    public class ConsoleLoggerService : ILoggerService
    {
        public void Log(string message) => Debug.Log(message);
    }
}