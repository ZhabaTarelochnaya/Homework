using System.Collections.Generic;

namespace NPCEventNotification.Scripts.Gameplay
{
    public class EventManager
    {
        List<GameEvent> gameEvents = new();
        public IEnumerator<GameEvent> GameEvents => gameEvents.GetEnumerator();
        
        public delegate void GameEventHandler(GameEvent e);
        public event GameEventHandler OnGameEvent;
        
        public void TriggerEvent(GameEvent e) => OnGameEvent?.Invoke(e);
    }
}