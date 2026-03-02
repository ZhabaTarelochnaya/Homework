using UnityEngine;

namespace _MultiplayerFPS.Scripts.Config.Weapon
{
    [CreateAssetMenu(fileName = "AssaultRiffleConfig", menuName = "ScriptableObjects/Weapons/AssaultRiffleConfig")]
    public class AssaultRiffleConfig : ScriptableObject, IWeaponConfig
    {
        [field: SerializeField] public int Damage { get; private set; }
        [field: SerializeField] public float FireRate { get; private set; } 
        [field: SerializeField] public float Range { get; private set; } = Mathf.Infinity;
        [field: SerializeField] public float ReloadTime { get; private set; }
        [field: SerializeField] public int MaxAmmo { get; private set; }
        [field: SerializeField] public LayerMask AimLayers { get; private set; }
        [field: SerializeField] public GameObject Prefab { get; private set; }
    }
}