using DefaultNamespace.Gameplay.World;
using UnityEngine;

namespace DefaultNamespace.Gameplay.Services
{
    public interface ICameraUser : IPositionUser
    {
        public Vector3 CameraRotation { get; set; }
    }
}