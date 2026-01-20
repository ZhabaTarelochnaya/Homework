using _TopDownShooter.Scripts.Utils.ServiceLocator;
using UnityEngine;

namespace _TopDownShooter.Scripts.Gameplay.Services
{
    public class MoveService : IService
    {
        public void MoveToDirection(Rigidbody rigidbody, Vector3 direction, float moveSpeed)
        {
            rigidbody.velocity = direction * moveSpeed;
        }
        public void Stop(Rigidbody rigidbody) => rigidbody.velocity = Vector3.zero;
    }
}