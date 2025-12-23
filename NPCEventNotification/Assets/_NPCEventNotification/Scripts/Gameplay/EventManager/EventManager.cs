using System.Collections.Generic;

namespace NPCEventNotification.Scripts.Gameplay
{
    public class EventManager
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