using System.Threading;
using DefaultNamespace.Gameplay.World;
using Utils.ServiceLocator;

namespace DefaultNamespace.Gameplay
{
    public static class GameplayServiceRegistrations
    {
        public static void Register()
        {
            var moveService = new MoveService();
            ServiceLocator.Current.Register(moveService);
        }

        public static void Unregister()
        {
            ServiceLocator.Current.Unregister<MoveService>();
        }
    }
}