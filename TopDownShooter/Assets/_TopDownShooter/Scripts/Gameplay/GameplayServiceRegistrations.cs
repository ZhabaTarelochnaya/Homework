using _TopDownShooter.Scripts.Controllers;
using _TopDownShooter.Scripts.Gameplay.Services;
using _TopDownShooter.Scripts.Utils.ServiceLocator;
using UnityEngine;

namespace _TopDownShooter.Scripts
{
    public static class GameplayServiceRegistrations
    {
        public static void Register(Camera camera)
        {
            var moveService = new MoveService();
            ServiceLocator.Current.Register(moveService);
            var inputService = new InputService();
            ServiceLocator.Current.Register(inputService);
            var cameraManager = new CameraManager(camera.transform);
            ServiceLocator.Current.Register(cameraManager);
        }

        public static void Unregister()
        {
            ServiceLocator.Current.Unregister<MoveService>();
            ServiceLocator.Current.Unregister<InputService>();
            ServiceLocator.Current.Unregister<CameraManager>();
        }
    }
}