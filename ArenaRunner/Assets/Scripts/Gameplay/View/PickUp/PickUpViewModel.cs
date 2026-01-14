using DefaultNamespace.Gameplay.Data.PickUp;
using UnityEngine;

namespace DefaultNamespace.Gameplay.View.PickUp
{
    public class PickUpViewModel
    {
        readonly PickUpDataProxy _pickUpDataProxy;
        public int ID { get => _pickUpDataProxy.ID; }
        public Vector3 Position { get => _pickUpDataProxy.Position; }

        public PickUpViewModel(PickUpDataProxy pickUpDataProxy)
        {
            _pickUpDataProxy = pickUpDataProxy;
        }
    }
}