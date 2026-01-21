using _TopDownShooter.Scripts.Controllers;
using _TopDownShooter.Scripts.Gameplay.Configs;
using _TopDownShooter.Scripts.Utils.ServiceLocator;
using UnityEngine;
using UnityEngine.AI;

namespace _TopDownShooter.Scripts.Gameplay.Services
{
    public class MoveService : IService
    {
        readonly CameraConfig _cameraConfig;

        public MoveService()
        {
            _cameraConfig = ServiceLocator.Current.Get<ConfigProviderService>().GetCameraConfig();
        }
        public void MoveToDirection(Rigidbody rigidbody, Vector3 direction, float moveSpeed)
        {
            rigidbody.velocity = direction * moveSpeed;
        }
        public void CameraRelativeMoveToDirection(Rigidbody rigidbody, Vector3 direction, float moveSpeed)
        {
            var relativeDirection = Quaternion.AngleAxis(_cameraConfig.Rotation.y, Vector3.up) * direction;
            MoveToDirection(rigidbody, relativeDirection, moveSpeed);
        }
        public void MoveToTarget(NavMeshAgent agent, Vector3 target) => agent.SetDestination(target);
        public void Stop(Rigidbody rigidbody) => rigidbody.velocity = Vector3.zero;
    }
}