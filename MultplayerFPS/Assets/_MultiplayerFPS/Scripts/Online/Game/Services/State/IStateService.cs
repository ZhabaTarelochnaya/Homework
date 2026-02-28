using _MultiplayerFPS.Scripts.State;
using _MultiplayerFPS.Scripts.Utils.ServiceLocator;

namespace _MultiplayerFPS.Scripts.Services.State
{
    public interface IStateService : IService
    {
        public GameState GameState { get; }

        public PlayerState GetPlayerState(uint netId);
    }
}