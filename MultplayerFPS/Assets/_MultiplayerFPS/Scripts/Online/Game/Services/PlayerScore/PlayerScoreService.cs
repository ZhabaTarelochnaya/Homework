using _MultiplayerFPS.Scripts.Services.State;
using _MultiplayerFPS.Scripts.State;

namespace _MultiplayerFPS.Scripts.Services
{
    public class PlayerScoreService : IPlayerScoreService
    {
        readonly GameState _gameState;
        
        public PlayerScoreService(IStateService stateService)
        {
            _gameState = stateService.GameState;
        }
        public void AddKill(uint playerNetId)
        {
            var oldScore = _gameState.PlayerScores[playerNetId];
            _gameState.PlayerScores[playerNetId] = new PlayerScore(oldScore.Kills + 1, oldScore.Deaths);
        }
        public void AddDeath(uint playerNetId)
        {
            var oldScore = _gameState.PlayerScores[playerNetId];
            _gameState.PlayerScores[playerNetId] = new PlayerScore(oldScore.Kills, oldScore.Deaths + 1);
        }
    }
}