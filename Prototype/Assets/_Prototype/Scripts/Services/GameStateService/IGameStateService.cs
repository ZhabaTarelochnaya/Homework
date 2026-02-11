using _Prototype.Scripts.Gameplay.Services.PickUpService;
using _Prototype.Scripts.Gameplay.State;
using _Prototype.Scripts.Utils.ServiceLocator;

namespace _Prototype.Scripts.Gameplay.Services.GameDataService
{
    public interface IGameStateService : IService
    {
        public GameState GameState { get; }
        public void AddPickUp(PickUp pickUp);
        public int CreateID();
    }
}