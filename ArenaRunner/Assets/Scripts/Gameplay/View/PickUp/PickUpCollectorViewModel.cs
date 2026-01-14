using DefaultNamespace.Gameplay.Data;
using Utils.ServiceLocator;

namespace DefaultNamespace.Gameplay.View.PickUp
{
    public class PickUpCollectorViewModel
    {
        PickUpCollectionService _pickUpCollectionService;
        public PickUpCollectorViewModel()
        {
            _pickUpCollectionService = ServiceLocator.Current.Get<PickUpCollectionService>();
        }

        public void Collect(int id) => _pickUpCollectionService.CollectPickUp(id);
    }
}