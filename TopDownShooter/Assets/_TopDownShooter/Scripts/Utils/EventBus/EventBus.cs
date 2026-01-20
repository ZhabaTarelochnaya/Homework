using System.Collections.Generic;
using _TopDownShooter.Scripts.Utils.ServiceLocator;

namespace _TopDownShooter.Scripts.Utils.EventBus
{
    public class EventBus : IService
    {
        List<GameEvent> gameEvents = new();
        public IEnumerable<GameEvent> GameEvents => gameEvents;
        
        public delegate void GameEventHandler(GameEvent e);
        public event GameEventHandler OnGameEvent;

        public void TriggerEvent(GameEvent e)
        {
            gameEvents.Add(e);
            OnGameEvent?.Invoke(e);
        }
    }
}