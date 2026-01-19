using System.Collections.Generic;
using System.Linq;
using Utils.EventBus;
using Utils.ServiceLocator;

namespace Gameplay.Services
{
    public class AnalyticsService : IService
    {
        readonly IEnumerable<GameEvent> _gameEvents;

        public AnalyticsService()
        {
            _gameEvents = ServiceLocator.Current.Get<EventBus>().GameEvents;
        }

        public string GetLastEvents(int number)
        {
            string result = "Last events:\n";
            _gameEvents.TakeLast(number)
                .ToList()
                .ForEach(e => result += $"{e}\n");
            return result;
        }

        public string GetEnemySpawnedCount()
        {
            var enemyCount = _gameEvents.Count(e => e.Name == EventName.EnemySpawned);
            return $"Enemies spawned: {enemyCount}";
        }
        public string GetPickedItemsCount()
        {
            var count = _gameEvents.Count(e => e.Name == EventName.ItemPicked);
            return $"Items picked: {count}";
        }
    }
}