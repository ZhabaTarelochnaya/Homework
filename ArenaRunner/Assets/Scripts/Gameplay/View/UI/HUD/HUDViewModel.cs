using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.AnalyticsTab;
using Utils.EventBus;
using Utils.ServiceLocator;

namespace DefaultNamespace
{
    public class HUDViewModel
    {
        EventBus _eventBus;
        public IEnumerable<GameEvent> GameEvents => _eventBus.GameEvents;

        public event EventBus.GameEventHandler OnGameEvent
        {
            add => _eventBus.OnGameEvent += value;
            remove => _eventBus.OnGameEvent -= value;
        }
        public AnalyticsTabViewModel AnalyticsTabViewModel { get; }
        public HUDViewModel()
        {
            AnalyticsTabViewModel = new AnalyticsTabViewModel();
            _eventBus = ServiceLocator.Current.Get<EventBus>();
        }
    }
}