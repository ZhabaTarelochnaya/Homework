using _MultiplayerFPS.Scripts.Utils.ServiceLocator;
using UnityEngine;

namespace _MultiplayerFPS.Scripts.Services.InputService
{
    public interface IInputService : IService
    {
        public Vector2 GetMove();
    }
}