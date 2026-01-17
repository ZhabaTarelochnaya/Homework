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
    }
}