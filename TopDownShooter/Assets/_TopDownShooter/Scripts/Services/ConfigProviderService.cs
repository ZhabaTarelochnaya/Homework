namespace _TopDownShooter.Scripts.Gameplay.Services
{
    public class ConfigProviderService
    {
        readonly GameConfig _gameConfig;

        public ConfigProviderService(GameConfig gameConfig)
        {
            _gameConfig = gameConfig;
        }
    }
}