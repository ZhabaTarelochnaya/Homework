using _Prototype.Scripts.Gameplay.Services.PickUpService;
using _Prototype.Scripts.Gameplay.View;
using _Prototype.Scripts.Utils.ServiceLocator;

namespace _Prototype.Scripts.Gameplay.Controllers.CollectorController
{
    public class CollectorController : ICollectorController
    {
        ICollectService _collectService;
        public CollectorController(ICollectorView collectorView)
        {
            _collectService = ServiceLocator.Current.Get<ICollectService>();
            collectorView.PickUpCollected += Collect;
        }
        public void Collect(int id) => _collectService.Collect(id);
    }
}