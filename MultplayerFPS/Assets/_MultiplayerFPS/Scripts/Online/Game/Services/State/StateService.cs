using System.Collections.Generic;
using _MultiplayerFPS.Scripts.State;

namespace _MultiplayerFPS.Scripts.Services.State
{
    public class StateService : IStateService
    {
        public GameState GameState { get; }
        
        public StateService(GameState gameState)
        {
            GameState = gameState;
        }
        
        public PlayerState GetPlayerState(uint netId)
        {
            return GameState.PlayerStates[netId];
        }
    }
}