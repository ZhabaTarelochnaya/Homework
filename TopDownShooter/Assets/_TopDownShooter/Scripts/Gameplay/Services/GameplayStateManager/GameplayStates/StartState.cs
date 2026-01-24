using _TopDownShooter.Scripts.Controllers;
using _TopDownShooter.Scripts.Gameplay.Controllers;
using _TopDownShooter.Scripts.Gameplay.Services;
using _TopDownShooter.Scripts.Utils.EventBus;
using _TopDownShooter.Scripts.Utils.ServiceLocator;
using _TopDownShooter.Scripts.Utils.StateMachine;
using _TopDownShooter.Scripts.View;
using UnityEngine;

namespace _TopDownShooter.Scripts.GameplayStates
{
    public class StartState : FSMState<GameplayStateName>
    {
        readonly EventBus _eventBus;
        readonly WindowManagerService _windowManager;
        PlayerView _playerView;
        EnemySpawnerView _enemySpawnerView;

        public StartState(PlayerView playerView, EnemySpawnerView enemySpawnerView) 
            : base(GameplayStateName.Start)
        {
            _playerView = playerView;
            _enemySpawnerView = enemySpawnerView;
            _eventBus = ServiceLocator.Current.Get<EventBus>();
            _windowManager = ServiceLocator.Current.Get<WindowManagerService>();
        }

        public override void OnEnter()
        {
            _windowManager.ClearPopups();
            
            BindUI();
            BindPlayer();
            BindEnemySpawner();
            
            var cameraManager = ServiceLocator.Current.Get<CameraManager>();
            cameraManager.SetTarget(_playerView.transform);
            
            var eventBus = ServiceLocator.Current.Get<EventBus>();
            UpdateUI(eventBus);
            _eventBus.TriggerEvent(new GameEvent(EventName.GameStateChanged,
                $"Entered gameplay {StateName}",
                StateName));
        }
        public override GameplayStateName GetNextState()
        {
            return GameplayStateName.Start;
        }
        void BindPlayer()
        {
            var playerController = new PlayerController(_playerView.Rigidbody, _playerView.HurtBox);
            _playerView.Bind(playerController);
            var weaponManager = ServiceLocator.Current.Get<WeaponManager>();
            var playerConfig = ServiceLocator.Current.Get<ConfigProviderService>().GetPlayerConfig();
            foreach (var weaponName in playerConfig.StartingWeapons)
            {
                weaponManager.Equip(weaponName);
            }
        }
        void BindEnemySpawner()
        {
            var enemyController = new EnemySpawnerController(_enemySpawnerView.SpawnPoints, 
                _enemySpawnerView.EnemiesParent, _playerView.transform);
            _enemySpawnerView.Bind(enemyController);
        }
        void BindUI()
        {
            var windowManager = ServiceLocator.Current.Get<WindowManagerService>();
            windowManager.OpenGameplayUI();
        }
        void UpdateUI(EventBus eventBus)
        {
            eventBus.TriggerEvent(new GameEvent(EventName.PlayerHurt,
                $"Player hp set",
                0, _playerView.HurtBox));
        }
    }
}