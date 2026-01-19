using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.AnalyticsTab;
using DefaultNamespace.Gameplay.Data;
using Gameplay.Services;
using UnityEngine;
using Utils.EventBus;
using Utils.ServiceLocator;

namespace DefaultNamespace
{
    public class HUDViewModel : IScreenViewModel
    {
        readonly EventBus _eventBus;
        readonly GameStateService _gameStateService;
        public IEnumerable<GameEvent> GameEvents => _eventBus.GameEvents;
        public WindowName Name => WindowName.HUD;
        public int CurrentHealth => _gameStateService.GameState.PlayerState.CurrentHealth;
        public int MaxScore => _gameStateService.GameState.WinScore;
        public GameObject HUDInstance { get; set; }
        public event EventBus.GameEventHandler OnGameEvent
        {
            add => _eventBus.OnGameEvent += value;
            remove => _eventBus.OnGameEvent -= value;
        }
        public AnalyticsTabViewModel AnalyticsTabViewModel { get; }
        public HUDViewModel()
        {
            AnalyticsTabViewModel = new AnalyticsTabViewModel();
            _eventBus = ServiceLocator.Current.Get<EventBus>();
            _gameStateService = ServiceLocator.Current.Get<GameStateService>();
        }

        public void Restart()
        {
            _eventBus.TriggerEvent(new GameEvent(EventName.GameStateChanged, 
                "Restart button was pressed",
                GameStateName.Init));
        }

        public void Pause()
        {
            _eventBus.TriggerEvent(new GameEvent(EventName.GameStateChanged, 
                "Pause button was pressed",
                GameStateName.Paused));
        }

        public void Resume()
        {
            _eventBus.TriggerEvent(new GameEvent(EventName.GameStateChanged, 
                "Resume button was pressed",
                GameStateName.Playing));
        }
        
        public void Close()
        {
            Object.Destroy(HUDInstance);
        }
    }
}