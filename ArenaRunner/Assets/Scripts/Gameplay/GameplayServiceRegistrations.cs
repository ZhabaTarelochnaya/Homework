using System.Threading;
using DefaultNamespace.Gameplay.Controllers;
using DefaultNamespace.Gameplay.Data;
using DefaultNamespace.Gameplay.World;
using Gameplay.Services;
using Utils.ServiceLocator;

namespace DefaultNamespace.Gameplay
{
    public static class GameplayServiceRegistrations
    {
        public static void Register(PlayerDataProxy playerDataProxy, ICamera camera)
        {
            var moveService = new MoveService();
            ServiceLocator.Current.Register(moveService);
            var cameraManager = new CameraManager(playerDataProxy, camera);
            ServiceLocator.Current.Register(cameraManager);
        }

        public static void Unregister()
        {
            ServiceLocator.Current.Unregister<MoveService>();
        }
    }
}