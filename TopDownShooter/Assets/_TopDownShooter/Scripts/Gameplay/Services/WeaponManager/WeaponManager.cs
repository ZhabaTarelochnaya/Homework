using System;
using System.Collections.Generic;
using _TopDownShooter.Scripts.Gameplay.Configs;
using _TopDownShooter.Scripts.Gameplay.Controllers.ShootStates;
using _TopDownShooter.Scripts.Gameplay.Services;
using _TopDownShooter.Scripts.Utils.EventBus;
using _TopDownShooter.Scripts.Utils.ServiceLocator;
using _TopDownShooter.Scripts.Utils.StateMachine;
using _TopDownShooter.Scripts.View;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = System.Random;

namespace _TopDownShooter.Scripts.Gameplay.Controllers
{
    public class WeaponManager : IService
    {
        readonly Transform _weaponPivot;
        readonly GameplayConfig _gameplayConfig;
        readonly ConfigProviderService _configProviderService;
        readonly InputService _inputService;
        readonly EventBus _eventBus;
        FSM<WeaponStateName> _fsm = new ();
        int _currentWeaponIndex = 0;
        List<WeaponConfig> _weaponConfigs = new();
        List<WeaponView> _weaponViews = new();
        public int CurrentAmmo { get; private set; } = 0;
        public int MaxAmmo => CurrentConfig.AmmunitionCapacity;
        public float ShootTimer { get; private set; } = 0;
        public WeaponView CurrentView { get; private set; }
        public WeaponConfig CurrentConfig { get; private set; }
        public event Action Shot;
        public event Action Reloaded;
        public event Action WeaponSwitched;
        public WeaponManager(Transform weaponPivot)
        {
            _weaponPivot = weaponPivot;
            _configProviderService = ServiceLocator.Current.Get<ConfigProviderService>();
            _eventBus = ServiceLocator.Current.Get<EventBus>();
            _gameplayConfig = _configProviderService.GetGameplayConfig();
            _inputService = ServiceLocator.Current.Get<InputService>();
            _fsm.AddState(new IdleState(_inputService, this))
                .AddState(new ReloadState(_inputService, this))
                .AddState(new ShootState(_inputService, this));
        }

        public void Equip(WeaponName name)
        {
            CurrentConfig = _configProviderService.GetWeaponConfig(name);
            _weaponConfigs.Add(CurrentConfig);
            var instance = Object.Instantiate(CurrentConfig.Prefab, _weaponPivot);
            if (CurrentView)
            {
                CurrentView.gameObject.SetActive(false);
            }
            CurrentView = instance.GetComponent<WeaponView>();
            CurrentView.Bind(this, CurrentConfig);
            _weaponViews.Add(CurrentView);
            Reload();
            _eventBus.TriggerEvent(new GameEvent(EventName.WeaponSwitched, 
                $"Switched to {name}"));
        }
        public void SwitchWeapon(int direction)
        {
            _currentWeaponIndex += direction;
            if (_currentWeaponIndex < 0)
            {
                _currentWeaponIndex = _weaponConfigs.Count - 1;
            }
            else if (_currentWeaponIndex >= _weaponConfigs.Count)
            {
                _currentWeaponIndex = 0;
            }
            CurrentConfig = _weaponConfigs[_currentWeaponIndex];
            CurrentView?.gameObject.SetActive(false);
            CurrentView = _weaponViews[_currentWeaponIndex];
            CurrentView.gameObject.SetActive(true);
            CurrentAmmo = 0;
            WeaponSwitched?.Invoke();
            _fsm.Transition(WeaponStateName.Reload);
            _eventBus.TriggerEvent(new GameEvent(EventName.WeaponSwitched, 
                $"Switched to {CurrentConfig.Name}"));
        }
        public void Update()
        {
            ShootTimer += Time.deltaTime;
            _fsm.Tick(Time.deltaTime);
        }
        public void Reload()
        {
            CurrentAmmo = MaxAmmo;
            Reloaded?.Invoke();
        }

        public void Shoot()
        {
            CurrentAmmo--;
            ShootTimer = 0;
            var spread = UnityEngine.Random.Range(-CurrentConfig.SpreadAngle, CurrentConfig.SpreadAngle);
            var direction = Quaternion.Euler(0, spread, 0) * CurrentView.ShootPosition.forward;
            if (Physics.Raycast(CurrentView.ShootPosition.position, direction, 
                    out RaycastHit hit, 1000f,_gameplayConfig.BulletMask))
            {
                var hurtBox = hit.collider.GetComponent<HurtBox>();
                if (hurtBox)
                {
                    hurtBox.TakeDamage(CurrentConfig.Damage);
                }
                var rigidbody = hit.collider.GetComponent<Rigidbody>();
                if (rigidbody)
                {
                    rigidbody.AddForceAtPosition(-hit.normal * CurrentConfig.Damage * 10, hit.point);
                }
            }
            Shot?.Invoke();
        }
    }
}