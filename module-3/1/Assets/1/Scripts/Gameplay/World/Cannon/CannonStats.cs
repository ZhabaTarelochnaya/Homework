using UnityEngine;

namespace _1.Gameplay.World.Cannon
{
    [CreateAssetMenu(fileName = "CannonStats", menuName = "ScriptableObjects/CannonStats")]
    public class CannonStats : ScriptableObject
    {
        [field: SerializeField] public float ProjectileSpeed { get; private set; }
        [field: SerializeField] public float ShootCoolDown { get; private set; }
        [field: SerializeField] public int ProjectileDamage { get; private set; }
    }
}