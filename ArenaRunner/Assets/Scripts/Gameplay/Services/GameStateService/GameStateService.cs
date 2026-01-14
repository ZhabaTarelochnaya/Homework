using Utils.EventBus;
using Utils.ServiceLocator;

namespace DefaultNamespace.Gameplay.Data
{
    public class GameStateService : IService
    {
        readonly EventBus _eventBus;
        public GameState GameState { get; }

        public GameStateService(GameState gameState, EventBus eventBus)
        {
            GameState = gameState;
            _eventBus = eventBus;
        }
        public void AddScore(int score)
        {
            GameState.Score += score;
            _eventBus.TriggerEvent(new GameEvent(EventName.ScoreChanged, 
                $"Score changed to {GameState.Score}"));
        }
    }
}