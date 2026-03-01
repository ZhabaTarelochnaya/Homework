using _MultiplayerFPS.Scripts.Components;
using UnityEngine;

namespace _MultiplayerFPS.Scripts.Config.Pickup
{
    [CreateAssetMenu(fileName = "MedKitConfig", menuName = "ScriptableObjects/Pickups/MedKitConfig")]
    public class MedKitConfig : PickupConfig
    {
        [field: SerializeField] public override PickupName Name { get; protected set; }
        [field: SerializeField] public override Components.Pickup Prefab { get; protected set; }
        [field: SerializeField] public override int Limit { get; protected set; } = 4;
        [field: SerializeField] public int Heal { get; protected set; } = 30;
        [field: SerializeField] public float HealDuration { get; protected set; } = 1f;
    }
}