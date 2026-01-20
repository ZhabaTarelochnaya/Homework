using _TopDownShooter.Scripts.Gameplay.Services;
using _TopDownShooter.Scripts.Utils.ServiceLocator;

namespace _TopDownShooter.Scripts
{
    public static class GameplayServiceRegistrations
    {
        public static void Register()
        {
            var moveService = new MoveService();
            ServiceLocator.Current.Register(moveService);
            var inputService = new InputService();
            ServiceLocator.Current.Register(inputService);
        }

        public static void Unregister()
        {
            ServiceLocator.Current.Unregister<MoveService>();
            ServiceLocator.Current.Unregister<InputService>();
        }
    }
}