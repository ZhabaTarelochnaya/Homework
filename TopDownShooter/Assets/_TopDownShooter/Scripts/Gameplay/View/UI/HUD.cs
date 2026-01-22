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
        [SerializeField] TMP_Text _healthText;
        [SerializeField] TMP_Text _killsText;

        public void Bind()
        {
            _eventBus = ServiceLocator.Current.Get<EventBus>();
            _sceneLoaderService = ServiceLocator.Current.Get<SceneLoaderService>();
            _eventBus.GameEventFired += EventBusOnGameEventFired;
        }
        void EventBusOnGameEventFired(GameEvent e)
        {
            switch (e.Name)
            {
                case EventName.PlayerHurt:
                    var currentHP = e.Args[0];
                    _healthText.text = $"Health: {currentHP}";
                    break;
                case EventName.EnemyKilled:
                    _killsText.text = $"Kills: {++killCount}";
                    break;
            }
        }
        public void OnRestartButtonPressed() => _sceneLoaderService.LoadGameplay();
    }
}