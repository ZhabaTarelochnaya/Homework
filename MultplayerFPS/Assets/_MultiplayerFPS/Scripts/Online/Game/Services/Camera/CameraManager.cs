using UnityEngine;

namespace _MultiplayerFPS.Scripts.Controllers
{
    public class CameraManager : ICameraManager
    {
        readonly Camera _camera;
        Vector3 _velocity;
        float _xRotation;
        public CameraManager(Camera camera)
        {
            _camera = camera;
        }
        public void FollowPosition(Transform target)
        {
            _camera.transform.position = target.position;
        }

        public void FollowRotation(float lookY, float playerYRotation)
        {
            _xRotation -= lookY;
            _xRotation = Mathf.Clamp(_xRotation, -80f, 80f);
            _camera.transform.rotation = Quaternion.Euler(_xRotation, playerYRotation, 0f);
        }
    }
}