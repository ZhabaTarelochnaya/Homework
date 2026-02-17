using UnityEngine;

namespace _Prototype.Scripts.Gameplay.Services
{
    public class CameraService : ICameraService
    {
        public bool IsFollowingTarget { get; set; } = true;
        public IPositionUser CurrentTarget { get; set; }
        public Camera CurrentCamera { get; }
        Vector3 velocity = Vector3.zero;
        public CameraService()
        {
            CurrentCamera = Camera.main;
        }
        public void LateUpdate()
        {
            if (!IsFollowingTarget) return;
            FollowTarget();
        }

        void FollowTarget()
        {
            var x = CurrentTarget.Position.x;
            var y = CurrentTarget.Position.y;
            var z = CurrentCamera.transform.position.z;
            CurrentCamera.transform.position = Vector3.SmoothDamp(CurrentCamera.transform.position, 
                new Vector3(x, y, z), ref velocity, 0.1f); 
        }
    }
}