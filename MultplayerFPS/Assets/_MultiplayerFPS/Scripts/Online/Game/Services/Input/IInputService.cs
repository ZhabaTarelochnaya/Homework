using _MultiplayerFPS.Scripts.Utils.ServiceLocator;
using UnityEngine;

namespace _MultiplayerFPS.Scripts.Services.InputService
{
    public interface IInputService : IService
    {
        public Vector2 GetMove();
        public bool GetJumpButtonDown();
        public Vector2 GetLook();
        public bool GetShootButtonDown();
        public bool GetShootButton();
        public bool GetHealButtonDown();
        public bool GetGrenadeButtonDown();
    }
}