using _TopDownShooter.Scripts.Utils.ServiceLocator;
using UnityEngine;

namespace _TopDownShooter.Scripts.Gameplay.Services
{
    public class InputService : IService
    {
        public Vector3 GetMoveDirection()
        {
            return new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        }

        public Vector3 GetRotateDirection()
        {
            return new Vector3(Input.GetAxisRaw("Mouse X"), 0, Input.GetAxisRaw("Mouse Y")).normalized;
        }

        public bool IsShooting()
        {
            return Input.GetButton("Fire1");
        }
    }
}