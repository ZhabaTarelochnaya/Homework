using System;
using System.Threading;
using _TopDownShooter.Scripts.GameplayStates;
using _TopDownShooter.Scripts.Utils.EventBus;
using _TopDownShooter.Scripts.Utils.ServiceLocator;
using _TopDownShooter.Scripts.Utils.StateMachine;
using _TopDownShooter.Scripts.View;
using UnityEngine;

namespace _TopDownShooter.Scripts.Gameplay.Services
{
    public class GameplayStateManager : IService, IDisposable
    {
        readonly PlayerView _playerView;
        readonly EnemySpawnerView _enemySpawnerView;
        readonly SceneLoaderService _sceneLoaderService;
        readonly EventBus _eventBus;
        FSM<GameplayStateName> _fsm = new ();
        int _killsTarget;

        public GameplayStateManager(PlayerView playerView, EnemySpawnerView enemySpawnerView)
        {
            _playerView = playerView;
            _enemySpawnerView = enemySpawnerView;
            _sceneLoaderService = ServiceLocator.Current.Get<SceneLoaderService>();  
            _eventBus = ServiceLocator.Current.Get<EventBus>();
        }

        public void Initialize()
        {
            _fsm.AddState(new StartState(_playerView, _enemySpawnerView))
                .AddState(new PlayingState())
                .AddState(new LoseState())
                .AddState(new WinState());
            _fsm.Tick(0f);
            var spawnerConfig = ServiceLocator.Current.Get<ConfigProviderService>().GetEnemySpawnerConfig();
            foreach (var waveConfig in spawnerConfig.WaveConfigs)
            {
                foreach (var enemyInWaveCountConfig in waveConfig.Composition)
                {
                    _killsTarget += enemyInWaveCountConfig.Count;
                }
            }
            _eventBus.GameEventFired += EventBusOnGameEventFired;
        }
        void EventBusOnGameEventFired(GameEvent e)
        {
            switch (e.Name)
            {
                case EventName.PlayerHurt:
                    if (_playerView.HurtBox.CurrentHealth != 0) break;
                    _eventBus.TriggerEvent(new GameEvent(EventName.Lost,
                        "Player lost"));
                    _fsm.Transition(GameplayStateName.Lose);
                    break;
                case EventName.EnemyKilled:
                    if (--_killsTarget != 0) break;
                    _eventBus.TriggerEvent(new GameEvent(EventName.Won,
                        "Player won"));
                    _fsm.Transition(GameplayStateName.Win);
                    break;
            }
        }

        public void Reload()
        {
            Time.timeScale = 1;
            _sceneLoaderService.LoadGameplay();
        }

        public void Dispose()
        {
            _eventBus.GameEventFired -= EventBusOnGameEventFired;
        }
    }
}