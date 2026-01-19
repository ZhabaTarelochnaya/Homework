using DefaultNamespace.Gameplay.Controllers;
using DefaultNamespace.Gameplay.Data;
using DefaultNamespace.Gameplay.World;
using Gameplay.Services;
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
            var pickUpSpawnService = new SpawnService(gameConfig);
            ServiceLocator.Current.Register(pickUpSpawnService);
            var pickUpCollectionService = new PickUpCollectionService();
            ServiceLocator.Current.Register(pickUpCollectionService);
            var playerStateService = new PlayerStateService(playerState);
            ServiceLocator.Current.Register(playerStateService);
            var windowManagerService = new WindowManagerService(gameConfig.UIConfig);
            ServiceLocator.Current.Register(windowManagerService);
            var analyticsService = new AnalyticsService();
            ServiceLocator.Current.Register(analyticsService);
            var enemyStrategyFactory = new EnemyStrategyFactory(gameConfig);
            ServiceLocator.Current.Register(enemyStrategyFactory);
        }

        public static void Unregister()
        {
            ServiceLocator.Current.Unregister<MoveService>();
            ServiceLocator.Current.Unregister<CameraManager>();
            ServiceLocator.Current.Unregister<SpawnService>();
            ServiceLocator.Current.Unregister<PickUpCollectionService>();
            ServiceLocator.Current.Unregister<PlayerStateService>();
            ServiceLocator.Current.Unregister<WindowManagerService>();
            ServiceLocator.Current.Unregister<AnalyticsService>();
            ServiceLocator.Current.Unregister<PlayerStateService>();
        }
    }
}