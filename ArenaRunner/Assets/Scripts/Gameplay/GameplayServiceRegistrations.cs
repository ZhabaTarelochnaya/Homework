using DefaultNamespace.Gameplay.Controllers;
using DefaultNamespace.Gameplay.Data;
using DefaultNamespace.Gameplay.World;
using Gameplay.Services.Camera;
using Utils.ServiceLocator;

namespace DefaultNamespace.Gameplay
{
    public static class GameplayServiceRegistrations
    {
        public static void Register(PlayerState playerState, ICamera camera, GameConfig gameConfig)
        {
            var moveService = new MoveService();
            ServiceLocator.Current.Register(moveService);
            var cameraManager = new CameraManager(playerState, camera);
            ServiceLocator.Current.Register(cameraManager);
            var pickUpSpawnService = new PickUpSpawnService(gameConfig.PickUps);
            ServiceLocator.Current.Register(pickUpSpawnService);
        }

        public static void Unregister()
        {
            ServiceLocator.Current.Unregister<MoveService>();
            ServiceLocator.Current.Unregister<CameraManager>();
            ServiceLocator.Current.Unregister<PickUpSpawnService>();
        }
    }
}