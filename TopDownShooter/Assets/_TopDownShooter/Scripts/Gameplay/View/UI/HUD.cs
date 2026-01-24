using System.Collections;
using _TopDownShooter.Scripts.Gameplay.Services;
using _TopDownShooter.Scripts.Utils.EventBus;
using _TopDownShooter.Scripts.Utils.ServiceLocator;
using TMPro;
using UnityEngine;

namespace _TopDownShooter.Scripts.View
{
    public class HUD : MonoBehaviour, IScreen
    {
        EventBus _eventBus;
        SceneLoaderService _sceneLoaderService;
        int killCount = 0;
        Coroutine _reloadCoroutine;
        [SerializeField] FillableBar _healthBar;
        [SerializeField] FillableBar _reloadBar;
        [SerializeField] RectTransform _reloadBarParent;
        [SerializeField] TMP_Text _killsText;

        public void Bind()
        {
            _eventBus = ServiceLocator.Current.Get<EventBus>();
            _sceneLoaderService = ServiceLocator.Current.Get<SceneLoaderService>();
            _eventBus.GameEventFired += EventBusOnGameEventFired;
            
            _reloadBarParent.gameObject.SetActive(false);
        }
        void EventBusOnGameEventFired(GameEvent e)
        {
            switch (e.Name)
            {
                case EventName.PlayerHurt:
                    var hurtBox = (HurtBox)e.Args[1];
                    Debug.Log(hurtBox);
                    _healthBar.FillPercent = (float)hurtBox.CurrentHealth / hurtBox.MaxHealth;
                    break;
                case EventName.EnemyKilled:
                    _killsText.text = $"Kills: {++killCount}";
                    break;
                case EventName.ReloadStarted:
                    var reloadTime = (float)e.Args[0];
                    _reloadBarParent.gameObject.SetActive(true);
                    _reloadCoroutine = StartCoroutine(FillReloadBar(reloadTime));
                    break;
                case EventName.ReloadStopped:
                    if (_reloadCoroutine != null)
                    {
                        StopCoroutine(_reloadCoroutine);
                    }
                    _reloadBarParent.gameObject.SetActive(false);
                    break;
            }
        }
        public void OnRestartButtonPressed() => _sceneLoaderService.LoadGameplay();

        IEnumerator FillReloadBar(float reloadTime)
        {
            float timer = 0;
            while (timer < reloadTime)
            {
                timer += Time.deltaTime;
                _reloadBar.FillPercent = timer / reloadTime;
                yield return null;
            }
        }

        public void Reset()
        {
            killCount = 0;
            _killsText.text = $"Kills: 0";
            if (_reloadCoroutine != null)
            {
                StopCoroutine(_reloadCoroutine);
            }
            _reloadBarParent.gameObject.SetActive(false);
        }
    }
}