using UnityEngine;

namespace _TopDownShooter.Scripts.Gameplay.Configs
{
    [CreateAssetMenu(fileName = "WeaponConfig", menuName = "ScriptableObjects/WeaponConfig")]
    public class WeaponConfig : ScriptableObject
    {
        [field: SerializeField] public WeaponName Name { get; private set; }
        [field: SerializeField] public int Damage { get; private set; }
        [field: SerializeField] public float FireRate { get; private set; }
        [field: SerializeField] public GameObject Prefab { get; private set; }
        [field: SerializeField] public AudioClip ShootSound { get; private set; }
        [field: SerializeField] public float FireSoundDelay { get; private set; }
    }
}