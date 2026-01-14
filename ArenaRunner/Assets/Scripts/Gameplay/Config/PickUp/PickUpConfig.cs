using DefaultNamespace.Gameplay.Data.PickUp;
using UnityEngine;

namespace DefaultNamespace.Gameplay.Data
{
    [CreateAssetMenu(fileName = "PickUpConfig", menuName = "ScriptableObjects/PickUpConfig")]
    public class PickUpConfig : ScriptableObject, IPickUpConfig
    {
        [field: SerializeField] public PickUpType Type { get; private set; }
        [field: SerializeField] public int Price { get; private set; }
        [field: SerializeField] public GameObject Prefab { get; private set; }

        public PickUpData Create()
        {
            var pickUpData = new PickUpData();
            pickUpData.Type = Type;
            pickUpData.Price = Price;
            return pickUpData;
        }
    }
}