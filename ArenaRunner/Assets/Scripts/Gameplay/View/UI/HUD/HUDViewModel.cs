using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.AnalyticsTab;
using DefaultNamespace.Gameplay.Data;
using Gameplay.Services;
using Utils.EventBus;
using Utils.ServiceLocator;

namespace DefaultNamespace
{
    public class HUDViewModel
    {
        readonly EventBus _eventBus;
        readonly PlayerStateService _playerStateService;
        public IEnumerable<GameEvent> GameEvents => _eventBus.GameEvents;
        public int CurrentHealth => _playerStateService.PlayerState.CurrentHealth;

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
            _playerStateService = ServiceLocator.Current.Get<PlayerStateService>();
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
    }
}