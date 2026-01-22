using _TopDownShooter.Scripts.Gameplay.Configs;
using _TopDownShooter.Scripts.Utils.ServiceLocator;
using UnityEngine;

namespace _TopDownShooter.Scripts.Gameplay.Services
{
    public class AimService : IService
    {
        readonly GameplayConfig _config;

        public AimService()
        {
            _config = ServiceLocator.Current.Get<ConfigProviderService>().GetGameplayConfig();
        }
        public void CameraRelativeAim(Rigidbody rigidbody, Vector3 aimPosition)
        {
            if (TryGetMousePosition(out var position, aimPosition))
            {
                var direction = position - rigidbody.position;
                direction.y = 0;
                rigidbody.transform.forward = direction;
            }
            rigidbody.angularVelocity = Vector3.zero;
        }
        bool TryGetMousePosition(out Vector3 position, Vector3 aimPosition)
        {
            var ray = Camera.main.ScreenPointToRay(aimPosition);

            if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, _config.AimGroundMask))
            {
                position = hitInfo.point;
                return true;
            }
            position = Vector3.zero;
            return false;
        }
    }
}