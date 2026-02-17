using _Prototype.Scripts.Gameplay.Controllers;
using _Prototype.Scripts.Gameplay.Services.PickUpService;
using _Prototype.Scripts.Gameplay.State;

namespace _Prototype.Scripts.Gameplay.Services.GameDataService
{
    public class GameStateService : IGameStateService
    {
        public GameState GameState { get; }
        
        public GameStateService(GameState gameState)
        {
            GameState = gameState;
        }
        public void AddPickUp(PickUp pickUp) => GameState.PickUps.Add(pickUp);
        public int CreateID() => GameState.CreateID();
        public void AddDamageArea(DamageAreaController damageArea) => GameState.DamageAreas.Add(damageArea);
    }
}