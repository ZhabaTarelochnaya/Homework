using _TopDownShooter.Scripts.Gameplay.Configs;
using _TopDownShooter.Scripts.Utils.ServiceLocator;

namespace _TopDownShooter.Scripts.Gameplay.Services
{
    public class ConfigProviderService : IService
    {
        readonly GameConfig _gameConfig;

        public ConfigProviderService(GameConfig gameConfig)
        {
            _gameConfig = gameConfig;
        }

        public PlayerConfig GetPlayerConfig()
        {
            return _gameConfig.GameplayConfig.PlayerConfig;
        }
    }
}