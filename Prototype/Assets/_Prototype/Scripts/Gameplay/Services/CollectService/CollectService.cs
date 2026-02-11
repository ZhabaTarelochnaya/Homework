using _Prototype.Scripts.Gameplay.Services.GameDataService;
using _Prototype.Scripts.Gameplay.View;
using _Prototype.Scripts.Utils.ServiceLocator;
using UnityEngine;

namespace _Prototype.Scripts.Gameplay.Services.PickUpService
{
    public class CollectService : ICollectService
    {
        readonly IGameStateService _gameStateService;
        
        public CollectService()
        {
            _gameStateService = ServiceLocator.Current.Get<IGameStateService>();
        }
        
        public void Collect(int id)
        {
            var pickUps = _gameStateService.GameState.PickUps;
            var index = pickUps.FindIndex(p => p.ID == id);
            pickUps[index].Collect();
            pickUps.RemoveAt(index);
        }
    }
}