using System.Linq;
using _TopDownShooter.Scripts.Gameplay.Configs;
using _TopDownShooter.Scripts.Gameplay.Configs.Enemies;
using _TopDownShooter.Scripts.Utils.ServiceLocator;

namespace _TopDownShooter.Scripts.Gameplay.Services
{
    public class ConfigProviderService : IService
    {
        public GameConfig GameConfig { get; }

        public ConfigProviderService(GameConfig gameConfig)
        {
            GameConfig = gameConfig;
        }

        public PlayerConfig GetPlayerConfig()
        {
            return GameConfig.GameplayConfig.PlayerConfig;
        }
        public CameraConfig GetCameraConfig() => GameConfig.GameplayConfig.CameraConfig;
        public GameplayConfig GetGameplayConfig() => GameConfig.GameplayConfig;

        public WeaponConfig GetWeaponConfig(WeaponName name)
        {
            var config = GameConfig.GameplayConfig.WeaponConfigs.FirstOrDefault(c => c.Name == name);
            return config;
        }

        public EnemyConfig GetEnemyConfig(EnemyName name)
        {
            var config = GameConfig.GameplayConfig.EnemyConfigs.FirstOrDefault(c => c.Name == name);
            return config;
        }

        public EnemySpawnerConfig GetEnemySpawnerConfig() => GameConfig.GameplayConfig.EnemySpawnerConfig;
    }
}