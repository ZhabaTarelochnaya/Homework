using System;
using _TopDownShooter.Scripts.Gameplay.Configs;
using _TopDownShooter.Scripts.Gameplay.Controllers;
using UnityEngine;

namespace _TopDownShooter.Scripts.View
{
    [RequireComponent(typeof(AudioSource))]
    public class WeaponView : MonoBehaviour
    {
        WeaponManager _weaponManager;
        AudioSource _audioSource;
        WeaponConfig _config;
        float _soundTimer;
        [field: SerializeField] public Transform ShootPosition { get; private set; }
        [field: SerializeField] public ParticleSystem Shot { get; private set; }
        public void Bind(WeaponManager weaponManager, WeaponConfig config)
        {
            _config = config;
            _weaponManager = weaponManager;
            _weaponManager.Shot += WeaponManagerOnShot;
        }

        void WeaponManagerOnShot()
        {
            if (gameObject.activeSelf && _soundTimer > _config.FireSoundDelay)
            {
                _audioSource.PlayOneShot(_audioSource.clip);
                Shot.Play();
                _soundTimer = 0;
            }
        }

        void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }
        void Update()
        {
            _soundTimer += Time.deltaTime;
            _weaponManager.Update();
        }

        void OnDestroy()
        {
            _weaponManager.Shot -= WeaponManagerOnShot;
        }
    }
}