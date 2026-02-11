using _Prototype.Scripts.Gameplay.Services.GameDataService;
using _Prototype.Scripts.Utils.EventBus;
using _Prototype.Scripts.Utils.ServiceLocator;

namespace _Prototype.Scripts.Gameplay.Services.PickUpService
{
    public class CollectService : ICollectService
    {
        readonly IGameStateService _gameStateService;
        readonly EventBus _eventBus;

        public CollectService()
        {
            _gameStateService = ServiceLocator.Current.Get<IGameStateService>();
            _eventBus = ServiceLocator.Current.Get<EventBus>();
        }
        
        public void Collect(int id)
        {
            var pickUps = _gameStateService.GameState.PickUps;
            var index = pickUps.FindIndex(p => p.ID == id);
            pickUps[index].Collect();
            pickUps.RemoveAt(index);
            _eventBus.TriggerEvent(new GameEvent(EventName.PickUpCollected, 
                $"Collected id:{id}"));
        }
    }
}