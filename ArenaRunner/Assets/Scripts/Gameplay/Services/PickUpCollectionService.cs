using System.Linq;
using Utils.EventBus;
using Utils.ServiceLocator;

namespace DefaultNamespace.Gameplay.Data
{
    public class PickUpCollectionService : IService
    {
        readonly GameStateService _gameStateService;
        readonly EventBus _eventBus;

        public PickUpCollectionService()
        {
            _gameStateService = ServiceLocator.Current.Get<GameStateService>();
            _eventBus = ServiceLocator.Current.Get<EventBus>();
        }

        public void CollectPickUp(int id)
        {
            var pickUp = _gameStateService.GameState.PickUps.FirstOrDefault(p => p.ID == id);
            pickUp.Collect();
            _gameStateService.GameState.PickUps.Remove(pickUp);
            _eventBus.TriggerEvent(new GameEvent(EventName.PickUpCollected, 
                $"Pick up {pickUp.Type} id:{pickUp.ID} collected"));
            _gameStateService.AddScore(pickUp.Score);
        }
    }
}