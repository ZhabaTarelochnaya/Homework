using UnityEngine;

namespace _MultiplayerFPS.Scripts.Config.Weapon
{
    [CreateAssetMenu(fileName = "AssaultRiffleConfig", menuName = "ScriptableObjects/Weapons/AssaultRiffleConfig")]
    public class AssaultRiffleConfig : ScriptableObject, IWeaponConfig
    {
        [field: SerializeField] public int Damage { get; private set; }
        [field: SerializeField] public float FireRate { get; private set; } 
        [field: SerializeField] public float Range { get; private set; } = Mathf.Infinity;
    }
}