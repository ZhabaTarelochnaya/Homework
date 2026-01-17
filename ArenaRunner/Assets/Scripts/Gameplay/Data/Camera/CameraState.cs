using Gameplay.Services.Camera;
using UnityEngine;

namespace DefaultNamespace.Gameplay.Data.Camera
{
    public class CameraState : ICamera
    {
        public Vector2 Position { get; set; }

        public CameraState(CameraData cameraData)
        {
            Position = cameraData.Position;
        }

        public CameraData ToData()
        {
            var cameraData = new CameraData();
            cameraData.Position = Position;
            return cameraData;
        }
    }
}