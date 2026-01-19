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
            _eventBus.OnGameEvent += EventBusOnGameEvent;
        }

        void EventBusOnGameEvent(GameEvent e)
        {
            if (e.Name == EventName.GameStateChanged)
            {
                GameState.GameStateName = (GameStateName)e.Args[0];
            }
        }
        public void ClearGameplayData()
        {
            GameState.Score = 0;
            GameState.Enemies.Clear();
            GameState.PickUps.Clear();
            GameState.PlayerState = null;
            GameState.CameraData = null;
            _eventBus.TriggerEvent(new GameEvent(EventName.ScoreChanged, 
                $"Score changed to {GameState.Score}",
                GameState.Score));
        }
        public void AddScore(int score)
        {
            GameState.Score += score;
            _eventBus.TriggerEvent(new GameEvent(EventName.ScoreChanged, 
                $"Score changed to {GameState.Score}",
                GameState.Score));
        }
    }
}