using _TopDownShooter.Scripts.Controllers;
using _TopDownShooter.Scripts.Gameplay.Controllers;
using _TopDownShooter.Scripts.Gameplay.Services;
using _TopDownShooter.Scripts.Utils.ServiceLocator;
using _TopDownShooter.Scripts.View;
using UnityEngine;

namespace _TopDownShooter.Scripts
{
    public static class GameplayServiceRegistrations
    {
        public static void Register(PlayerView playerView, EnemySpawnerView enemySpawnerView)
        {
            var moveService = new MoveService();
            ServiceLocator.Current.Register(moveService);
            var inputService = new InputService();
            ServiceLocator.Current.Register(inputService);
            var cameraManager = new CameraManager();
            ServiceLocator.Current.Register(cameraManager);
            var aimService = new AimService();
            ServiceLocator.Current.Register(aimService);
            var weaponManager = new WeaponManager(playerView.transform);
            ServiceLocator.Current.Register(weaponManager);
            
            // Must be last.
            var gameplayStateManager = new GameplayStateManager(playerView, enemySpawnerView);
            ServiceLocator.Current.Register(gameplayStateManager);
        }

        public static void Unregister()
        {
            ServiceLocator.Current.Unregister<MoveService>();
            ServiceLocator.Current.Unregister<InputService>();
            ServiceLocator.Current.Unregister<CameraManager>();
            ServiceLocator.Current.Unregister<AimService>();
            ServiceLocator.Current.Unregister<WeaponManager>();
            ServiceLocator.Current.Unregister<GameplayStateManager>();
        }
    }
}