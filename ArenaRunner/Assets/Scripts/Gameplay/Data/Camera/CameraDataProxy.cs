using Gameplay.Services;
using UnityEngine;

namespace DefaultNamespace.Gameplay.Data.Camera
{
    public class CameraDataProxy : ICamera
    {
        readonly CameraData _cameraData;
        public Vector3 Position { get => _cameraData.Position; set => _cameraData.Position = value; }
        public Vector3 Rotation { get => _cameraData.Rotation; set => _cameraData.Rotation = value; }

        public CameraDataProxy(CameraData cameraData)
        {
            _cameraData = cameraData;
        }
    }
}