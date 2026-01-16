using DefaultNamespace.Gameplay.Data.PickUp;
using DefaultNamespace.Gameplay.World;
using UnityEngine;
using Utils.ServiceLocator;

namespace DefaultNamespace.Gameplay.Data
{
    [CreateAssetMenu(fileName = "PickUpConfig", menuName = "ScriptableObjects/PickUpConfig")]
    public class PickUpConfig : ScriptableObject
    {
        [field: SerializeField] public PickUpType Type { get; private set; }
        [field: SerializeField] public int Score { get; private set; }
        [field: SerializeField] public GameObject Prefab { get; private set; }

        public PickUpState Create()
        {
            var pickUpData = new PickUpData();
            pickUpData.Type = Type;
            pickUpData.Score = Score;
            var idService = ServiceLocator.Current.Get<IDService>();
            pickUpData.ID = idService.CreateID();
            var pickUpState = new PickUpState(pickUpData);
            return pickUpState;
        }
    }
}