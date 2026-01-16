using UnityEngine;

namespace Gameplay.Services.Camera
{
    public interface ICamera
    {
        public Vector3 Position { get; set; }
        public Quaternion Rotation { get; set; }
    }
}