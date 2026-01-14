using DefaultNamespace.Gameplay.World;
using UnityEngine;

namespace DefaultNamespace.Gameplay.Data.PickUp
{
    public class PickUpDataProxy : IPositionUser
    {
        readonly PickUpData _pickUpData;
        public PickUpType Type { get => _pickUpData.Type; set => _pickUpData.Type = value; }
        public int Price { get => _pickUpData.Price; set => _pickUpData.Price = value; }
        public Vector3 Position { get => _pickUpData.Position; set => _pickUpData.Position = value; }
        public int ID { get => _pickUpData.ID; set => _pickUpData.ID = value; }

        public PickUpDataProxy(PickUpData pickUpData)
        {
            _pickUpData = pickUpData;
        }
    }
}