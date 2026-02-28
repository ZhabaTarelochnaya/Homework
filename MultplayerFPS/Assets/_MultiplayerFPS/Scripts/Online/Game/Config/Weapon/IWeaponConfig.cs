using UnityEngine;

namespace _MultiplayerFPS.Scripts.Config.Weapon
{
    public interface IWeaponConfig
    {
        public int Damage { get; }
        public float FireRate { get; }
        public float Range { get; }
        public float ReloadTime { get; }
        public int MaxAmmo { get; }
        public GameObject Prefab { get; }
    }
}