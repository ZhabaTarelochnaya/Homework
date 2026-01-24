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
        GameplayStateManager _gameplayStateManager;
        int killCount = 0;
        [SerializeField] FillableBar _healthBar;
        [SerializeField] ChosenWeaponUI _chosenWeaponUI;
        [SerializeField] TMP_Text _killsText;

        public void Bind()
        {
            _eventBus = ServiceLocator.Current.Get<EventBus>();
            _gameplayStateManager = ServiceLocator.Current.Get<GameplayStateManager>();
            _chosenWeaponUI.Bind();
            _eventBus.GameEventFired += EventBusOnGameEventFired;
        }
        void EventBusOnGameEventFired(GameEvent e)
        {
            switch (e.Name)
            {
                case EventName.PlayerHurt:
                    var hurtBox = (HurtBox)e.Args[1];
                    _healthBar.FillPercent = (float)hurtBox.CurrentHealth / hurtBox.MaxHealth;
                    break;
                case EventName.EnemyKilled:
                    _killsText.text = $"Kills: {++killCount}";
                    break;
            }
        }
        public void OnRestartButtonPressed() => _gameplayStateManager.Reload();
        public void Reset()
        {
            killCount = 0;
            _killsText.text = $"Kills: 0";
            _eventBus.GameEventFired -= EventBusOnGameEventFired;
            _chosenWeaponUI.Reset();
        }
    }
}