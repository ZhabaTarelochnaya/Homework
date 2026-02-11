using _Prototype.Scripts.Gameplay.Services.GameDataService;
using _Prototype.Scripts.Gameplay.State;
using _Prototype.Scripts.Utils.ServiceLocator;

namespace _Prototype.Scripts.Gameplay.Services.PickUpService
{
    public interface ICollectService : IService
    {
        public void Collect(int id);
    }
}