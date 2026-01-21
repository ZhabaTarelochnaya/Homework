using _TopDownShooter.Scripts.Controllers;
using _TopDownShooter.Scripts.Gameplay.Controllers;
using _TopDownShooter.Scripts.Gameplay.Services;
using _TopDownShooter.Scripts.Utils.ServiceLocator;
using UnityEngine;

namespace _TopDownShooter.Scripts
{
    public static class GameplayServiceRegistrations
    {
        public static void Register(Transform weaponPivot)
        {
            var moveService = new MoveService();
            ServiceLocator.Current.Register(moveService);
            var inputService = new InputService();
            ServiceLocator.Current.Register(inputService);
            var cameraManager = new CameraManager();
            ServiceLocator.Current.Register(cameraManager);
            var aimService = new AimService();
            ServiceLocator.Current.Register(aimService);
            var weaponManager = new WeaponManager(weaponPivot);
            ServiceLocator.Current.Register(weaponManager);
        }

        public static void Unregister()
        {
            ServiceLocator.Current.Unregister<MoveService>();
            ServiceLocator.Current.Unregister<InputService>();
            ServiceLocator.Current.Unregister<CameraManager>();
            ServiceLocator.Current.Unregister<AimService>();
            ServiceLocator.Current.Unregister<WeaponManager>();
        }
    }
}