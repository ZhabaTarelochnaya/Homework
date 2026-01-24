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

        public Vector3 GetMousePosition()
        {
            return Input.mousePosition;
        }

        public bool IsShooting()
        {
            return Input.GetButton("Fire1");
        }

        public int SwitchWeapon()
        {
            var input = Input.GetAxisRaw("Mouse ScrollWheel");
            if (input > 0) return 1;
            if (input < 0) return -1;
            return 0;
        }
    }
}