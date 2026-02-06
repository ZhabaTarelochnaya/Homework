using _Prototype.Scripts.Utils.ServiceLocator;
using UnityEngine;

namespace _Prototype.Scripts.Gameplay.Services
{
    public interface ICameraService : IService
    {
        public bool IsFollowingTarget { get; set; } 
        public IPositionUser CurrentTarget { get; set; }
        public Camera CurrentCamera { get; }
        public void LateUpdate();
    }
}