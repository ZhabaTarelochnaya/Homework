using _TopDownShooter.Scripts.Gameplay.Configs;
using _TopDownShooter.Scripts.Gameplay.Services;
using _TopDownShooter.Scripts.Utils.ServiceLocator;
using _TopDownShooter.Scripts.View;
using UnityEngine;

namespace _TopDownShooter.Scripts.Gameplay.Controllers
{
    public class WeaponManager : IService
    {
        readonly Transform _weaponPivot;
        readonly GameplayConfig _gameplayConfig;
        readonly ConfigProviderService _configProviderService;
        readonly InputService _inputService;
        AudioSource _audioSource;
        float _timer = 0;
        float _soundTimer = 0;
        public WeaponView CurrentView { get; private set; }
        public WeaponConfig CurrentConfig { get; private set; }

        public WeaponManager(Transform weaponPivot)
        {
            _weaponPivot = weaponPivot;
            _configProviderService = ServiceLocator.Current.Get<ConfigProviderService>();
            _gameplayConfig = _configProviderService.GetGameplayConfig();
            _inputService = ServiceLocator.Current.Get<InputService>();
        }

        public void Equip(WeaponName name)
        {
            CurrentConfig = _configProviderService.GetWeaponConfig(name);
            var instance = Object.Instantiate(CurrentConfig.Prefab, _weaponPivot);
            CurrentView = instance.GetComponent<WeaponView>();
            _audioSource = CurrentView.AudioSource;
            _audioSource.clip = CurrentConfig.ShootSound;
            CurrentView.Bind(this);
        }
        public void Update()
        {
            _timer += Time.deltaTime;
            _soundTimer += Time.deltaTime;
            if (_inputService.IsShooting() && _timer > 1 / CurrentConfig.FireRate)
            {
                if (_soundTimer > CurrentConfig.FireSoundDelay)
                {
                    _audioSource.PlayOneShot(_audioSource.clip);
                    _soundTimer = 0;
                }
                Shoot();
                _timer = 0;
            }
        }
        public void Shoot()
        {
            if (Physics.Raycast(CurrentView.ShootPosition.position,CurrentView.ShootPosition.forward, 
                    out RaycastHit hit, 1000f,_gameplayConfig.BulletMask))
            {
                var hurtBox = hit.collider.GetComponent<HurtBox>();
                if (!hurtBox) return;
                hurtBox.TakeDamage(CurrentConfig.Damage);
            }
        }
    }
}