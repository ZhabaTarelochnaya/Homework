using DefaultNamespace.Gameplay.World;
using UnityEngine;

namespace DefaultNamespace.Gameplay.Services
{
    public interface ICameraUser : IPositionUser
    {
        public Quaternion CameraRotation { get; set; }
    }
}