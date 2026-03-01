using _MultiplayerFPS.Scripts.Components;
using UnityEngine;

namespace _MultiplayerFPS.Scripts.Config.Pickup
{
    [CreateAssetMenu(fileName = "GrenadeConfig", menuName = "ScriptableObjects/Pickups/GrenadeConfig")]
    public class GrenadeConfig : PickupConfig
    {
        [field: SerializeField] public override PickupName Name { get; protected set; } = PickupName.Grenade;
        [field: SerializeField] public override Components.Pickup Prefab { get; protected set; }
        [field: SerializeField] public override int Limit { get; protected set; } = 4;
        [field: SerializeField] public Grenade GrenadePrefab { get; protected set; }
        [field: SerializeField] public int MinDamage { get; private set; } = 20;
        [field: SerializeField] public float MaxDamage { get; private set; } = 100;
        [field: SerializeField] public float ExplosionDelay  { get; private set; }
        [field: SerializeField] public float ThrowDuration { get; private set; } = 1f;
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public float ExplosionDuration { get; private set; } = 3f;
        [field: SerializeField] public int Radius { get; private set; }
        [field: SerializeField] public LayerMask HarmedLayers { get; private set; }
        [field: SerializeField] public LayerMask DamageBlockingLayers { get; private set; }
    }
}