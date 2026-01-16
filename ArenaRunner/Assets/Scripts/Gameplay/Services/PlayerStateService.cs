using DefaultNamespace.Gameplay.Data;
using Utils.ServiceLocator;

namespace Gameplay.Services
{
    public class PlayerStateService : IService
    {
        public PlayerState PlayerState;

        public PlayerStateService(PlayerState playerState)
        {
            PlayerState = playerState;
        }
    }
}