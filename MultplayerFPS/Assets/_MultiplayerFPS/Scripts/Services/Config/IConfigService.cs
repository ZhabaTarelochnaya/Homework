using _MultiplayerFPS.Scripts.Utils.ServiceLocator;
using UnityEngine;

namespace _MultiplayerFPS.Scripts.Services.Config
{
    public interface IConfigService : IService
    {
        public T Get<T>() where T : ScriptableObject;
    }
}