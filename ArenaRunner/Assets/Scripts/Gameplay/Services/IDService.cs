using DefaultNamespace.Gameplay.Data;
using Utils.ServiceLocator;

namespace DefaultNamespace.Gameplay.World
{
    public class IDService : IService
    {
        readonly GameState _gameState;

        public IDService(GameState gameState)
        {
            _gameState = gameState;
        }
        public int CreateID() => _gameState.CreateID();
    }
}