using _Prototype.Scripts.Gameplay.Services.GameDataService;
using _Prototype.Scripts.Gameplay.View;
using _Prototype.Scripts.Utils.ServiceLocator;

namespace _Prototype.Scripts.Gameplay.Services.PickUpService
{
    public abstract class PickUp
    {
        public int ID { get; }

        protected PickUp(IPickUpView pickUpView)
        {
            var gameStateService = ServiceLocator.Current.Get<IGameStateService>();
            ID = gameStateService.CreateID();
            pickUpView.ID = ID;
        }
        public abstract void Collect();
    }
}