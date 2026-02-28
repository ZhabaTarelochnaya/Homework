using UnityEngine;

namespace _MultiplayerFPS.Scripts.Controllers
{
    public class CameraManager : ICameraManager
    {
        Vector3 _velocity;
        float _xRotation;

        public Camera CurrentCamera { get; set; }
        
        public CameraManager(Camera camera)
        {
            CurrentCamera = camera;
        }

        public void FollowPosition(Transform target)
        {
            CurrentCamera.transform.position = target.position;
        }

        public void FollowRotation(float lookY, float playerYRotation)
        {
            _xRotation -= lookY;
            _xRotation = Mathf.Clamp(_xRotation, -60f, 60f);
            CurrentCamera.transform.rotation = Quaternion.Euler(_xRotation, playerYRotation, 0f);
        }
    }
}