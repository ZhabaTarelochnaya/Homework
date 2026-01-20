using _TopDownShooter.Scripts.Gameplay.Configs;
using _TopDownShooter.Scripts.Gameplay.Services;
using _TopDownShooter.Scripts.Utils.ServiceLocator;
using UnityEngine;

namespace _TopDownShooter.Scripts.Controllers
{
    public class CameraManager : IService
    {
        CameraConfig _config;
        Transform _camera;
        Transform _target;
        Vector3 _velocity = Vector3.zero;
        public bool IsFollowingTarget { get; set; } = true;
        public CameraManager()
        {
            _camera = Camera.main.transform;
            _config = ServiceLocator.Current.Get<ConfigProviderService>().GetCameraConfig();
            _camera.rotation = Quaternion.Euler(_config.Rotation);
        }

        public void SetTarget(Transform target) => _target = target;

        public void FollowTarget(Transform target)
        {
            if (!IsFollowingTarget) return;
            _camera.position = Vector3.SmoothDamp(_camera.position, 
                target.position + _config.Offset, ref _velocity, 0.2f);
        }
    }
}