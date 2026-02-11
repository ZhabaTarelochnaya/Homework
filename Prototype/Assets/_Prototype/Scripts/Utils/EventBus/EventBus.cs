using System.Collections.Generic;
using _Prototype.Scripts.Utils.ServiceLocator;

namespace _Prototype.Scripts.Utils.EventBus
{
    public class EventBus : IService
    {
        List<GameEvent> gameEvents = new();
        public IEnumerable<GameEvent> GameEvents => gameEvents;
        
        public delegate void GameEventHandler(GameEvent e);
        public event GameEventHandler GameEventFired;

        public void TriggerEvent(GameEvent e)
        {
            gameEvents.Add(e);
            GameEventFired?.Invoke(e);
        }
    }
}