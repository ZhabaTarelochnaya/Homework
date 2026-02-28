using _MultiplayerFPS.Scripts.Components;
using UnityEngine;

namespace _MultiplayerFPS.Scripts.Config.Pickup
{
    [CreateAssetMenu(fileName = "MedKitConfig", menuName = "ScriptableObjects/Pickups/MedKitConfig")]
    public class MedKitConfig : PickupConfig
    {
        [field: SerializeField] public override PickupName Name { get; protected set; }
        [field: SerializeField] public override Components.Pickup Prefab { get; protected set; }
    }
}