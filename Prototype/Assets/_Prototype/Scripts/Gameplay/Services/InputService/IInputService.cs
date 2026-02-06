using _Prototype.Scripts.Utils.ServiceLocator;
using UnityEngine;

namespace _Prototype.Scripts.Gameplay.Services.InputService
{
    public interface IInputService : IService
    {
        public Vector2 GetMovementInput();
    }
}