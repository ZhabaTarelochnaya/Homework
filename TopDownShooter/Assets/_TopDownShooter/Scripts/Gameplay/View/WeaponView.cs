using _TopDownShooter.Scripts.Gameplay.Controllers;
using UnityEngine;

namespace _TopDownShooter.Scripts.View
{
    [RequireComponent(typeof(AudioSource))]
    public class WeaponView : MonoBehaviour
    {
        WeaponManager _weaponManager;
        public AudioSource AudioSource { get; private set; }
        [field: SerializeField] public Transform ShootPosition { get; private set; }
        public void Bind(WeaponManager weaponManager)
        {
            _weaponManager = weaponManager;
        }
        void Awake()
        {
            AudioSource = GetComponent<AudioSource>();
        }
        void Update()
        {
            _weaponManager.Update();
        }
    }
}