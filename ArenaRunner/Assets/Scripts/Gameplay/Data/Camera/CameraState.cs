using Gameplay.Services.Camera;
using UnityEngine;

namespace DefaultNamespace.Gameplay.Data.Camera
{
    public class CameraState : ICamera
    {
        public Vector3 Position { get; set; }
        public Quaternion Rotation { get; set; }

        public CameraState(CameraData cameraData)
        {
            Position = cameraData.Position;
            Rotation = Quaternion.Euler(cameraData.Rotation);
        }

        public CameraData ToData()
        {
            var cameraData = new CameraData();
            cameraData.Position = Position;
            cameraData.Rotation = Rotation.eulerAngles;
            return cameraData;
        }
    }
}