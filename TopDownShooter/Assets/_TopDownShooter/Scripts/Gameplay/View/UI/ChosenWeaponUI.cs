using System;
using System.Collections;
using _TopDownShooter.Scripts.Gameplay.Controllers;
using _TopDownShooter.Scripts.Utils.EventBus;
using _TopDownShooter.Scripts.Utils.ServiceLocator;
using UnityEngine;

namespace _TopDownShooter.Scripts.View
{
    public class ChosenWeaponUI : MonoBehaviour
    {
        EventBus _eventBus;
        WeaponManager _weaponManager;
        Coroutine _reloadCoroutine;
        [SerializeField] FillableBar _fillBar;

        public void Bind()
        {
            _eventBus = ServiceLocator.Current.Get<EventBus>();
            _weaponManager = ServiceLocator.Current.Get<WeaponManager>();
            _eventBus.GameEventFired += EventBusOnGameEventFired;
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