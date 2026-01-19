using Gameplay.Services;
using Utils.EventBus;
using Utils.ServiceLocator;

namespace DefaultNamespace.AnalyticsTab
{
    public class AnalyticsTabViewModel
    {
        readonly EventBus _eventBus;
        readonly AnalyticsService _analyticsService;

        public event EventBus.GameEventHandler OnGameEvent
        {
            add => _eventBus.OnGameEvent += value;
            remove => _eventBus.OnGameEvent -= value;
        }
        public AnalyticsTabViewModel()
        {
            _eventBus = ServiceLocator.Current.Get<EventBus>();
            _analyticsService = ServiceLocator.Current.Get<AnalyticsService>();
        }
        public string GetLastEvents(int number) => _analyticsService.GetLastEvents(number);
        public string GetEnemySpawnedCount() => _analyticsService.GetEnemySpawnedCount();
        public string GetPickedItemsCount() => _analyticsService.GetPickedItemsCount();
    }
}