using System;
using System.Collections;
using _TopDownShooter.Scripts.Gameplay.Controllers;
using _TopDownShooter.Scripts.Utils.EventBus;
using _TopDownShooter.Scripts.Utils.ServiceLocator;
using TMPro;
using UnityEngine;

namespace _TopDownShooter.Scripts.View
{
    public class ChosenWeaponUI : MonoBehaviour
    {
        EventBus _eventBus;
        WeaponManager _weaponManager;
        Coroutine _reloadCoroutine;
        [SerializeField] FillableBar _fillBar;
        [SerializeField] TMP_Text _ammoText;

        public void Bind()
        {
            _eventBus = ServiceLocator.Current.Get<EventBus>();
            _weaponManager = ServiceLocator.Current.Get<WeaponManager>();
            _eventBus.GameEventFired += EventBusOnGameEventFired;
            _weaponManager.Shot += WeaponManagerOnUpdateAmmo;
            _weaponManager.Reloaded += WeaponManagerOnUpdateAmmo;
            _weaponManager.WeaponSwitched += WeaponManagerOnUpdateAmmo;
        }

        void WeaponManagerOnUpdateAmmo()
        {
            _ammoText.text = $"Ammo: {_weaponManager.CurrentAmmo} / {_weaponManager.MaxAmmo}";
        }

        void OnDestroy()
        {
            if (_eventBus == null) return;
            _eventBus.GameEventFired -= EventBusOnGameEventFired;
        }

        void EventBusOnGameEventFired(GameEvent e)
        {
            switch (e.Name)
            {
                case EventName.WeaponSwitched:
                    _fillBar.BackgroundSprite = _weaponManager.CurrentConfig.Sprite;
                    break;
                case EventName.ReloadStarted:
                    var reloadTime = (float)e.Args[0];
                    _reloadCoroutine = StartCoroutine(FillReloadBar(reloadTime));
                    break;
                case EventName.ReloadStopped:
                    if (_reloadCoroutine != null)
                    {
                        StopCoroutine(_reloadCoroutine);
                    }
                    _fillBar.FillPercent = 0;
                    break;
            }
        }
        
        IEnumerator FillReloadBar(float reloadTime)
        {
            float timer = reloadTime;
            while (timer > 0)
            {
                timer -= Time.deltaTime;
                _fillBar.FillPercent = timer / reloadTime;
                yield return null;
            }
        }

        public void Reset()
        {
            if (_reloadCoroutine != null)
            {
                StopCoroutine(_reloadCoroutine);
            }
            _fillBar.FillPercent = 0;
        }
    }
}