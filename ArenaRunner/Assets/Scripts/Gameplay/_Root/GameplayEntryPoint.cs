using System;
using DefaultNamespace.Gameplay.Data;
using DefaultNamespace.Gameplay.Data.Camera;
using DefaultNamespace.Gameplay.GameplayStates;
using DefaultNamespace.Gameplay.View;
using DefaultNamespace.Gameplay.View.Camera;
using DefaultNamespace.Gameplay.View.PickUp;
using DefaultNamespace.Gameplay.World;
using DefaultNamespace.Gameplay.World.Player;
using UnityEngine;
using Utils.EventBus;
using Utils.FiniteStateMachine;
using Utils.ServiceLocator;

namespace DefaultNamespace.Gameplay
{
    public class GameplayEntryPoint : MonoBehaviour
    {
        bool isBound;
        FSM<GameStateName> _gameStateFsm = new ();
        EventBus _eventBus;
        GameplayUIView _gameplayUIView;
        [SerializeField] PlayerView _playerView;
        [SerializeField] CameraView _cameraView;
        [SerializeField] Transform _pickUps;
        [SerializeField] EnemySpawnerView _enemySpawnerView;
        public void Bind(UIRoot uiRoot, GameConfig gameConfig, GameState gameState,
            ReloadGameplayCommand reloadGameplayCommand)
        {
            isBound = true;
            _eventBus = ServiceLocator.Current.Get<EventBus>();
            
            var playerData = gameConfig.CreatePlayerData();
            var playerState = new PlayerState(playerData);
            var cameraData = new CameraData();
            var cameraState = new CameraState(cameraData);

            
            GameplayServiceRegistrations.Register(playerState, cameraState, gameConfig);

            BindPlayer(playerState);
            BindCamera(playerState);
            BindEnemySpawnPoint();
            BindPickUps(gameConfig, gameState);
            BindUI(uiRoot, gameConfig);

            gameState.GameStateName = GameStateName.Playing;
            _gameStateFsm.AddState(new PlayingState())
                .AddState(new InitState(reloadGameplayCommand))
                .AddState(new LoseState())
                .AddState(new WinState())
                .AddState(new PausedState());
            
            _eventBus.OnGameEvent += EventBusOnGameEvent;
        }

        void EventBusOnGameEvent(GameEvent e)
        {
            _gameStateFsm.Tick(0f);
        }

        void OnDestroy()
        {
            if (!isBound) return;
            if (_gameStateFsm.CurrentState.StateName != GameStateName.Init)
            {
                Destroy(_gameplayUIView.gameObject);
            }
            var gameStateService = ServiceLocator.Current.Get<GameStateService>();
            gameStateService.ClearGameplayData();
            GameplayServiceRegistrations.Unregister();
            _eventBus.OnGameEvent -= EventBusOnGameEvent;
        }

        void BindPlayer(PlayerState playerState)
        {
            var playerController = new PlayerController(playerState);
            var playerViewModel = new PlayerViewModel(playerState, playerController);
            _playerView.Bind(playerViewModel);
        }
        void BindCamera(PlayerState playerState)
        {
            var cameraViewModel = new CameraViewModel(playerState);
            _cameraView.Bind(cameraViewModel);
        }

        void BindEnemySpawnPoint()
        {
            var enemySpawnerViewModel = new EnemySpawnerViewModel();
            _enemySpawnerView.Bind(enemySpawnerViewModel);
        }
        void BindUI(UIRoot uiRoot, GameConfig gameConfig)
        {
            if (uiRoot.GetComponentInChildren<GameplayUIView>() != null) return;
            var instance = Instantiate(gameConfig.UIConfig.GameplayUI, uiRoot.transform);
            _gameplayUIView = instance.GetComponent<GameplayUIView>();
            var gameplayUIViewModel = new GameplayUIViewModel();
            _gameplayUIView.Bind(gameplayUIViewModel);
        }
        void BindPickUps(GameConfig gameConfig, GameState gameState)
        {
            foreach (var pickUp in _pickUps.GetComponentsInChildren<PickUpView>())
            {
                var config = gameConfig.GetPickUpConfig(pickUp.PickUpType);
                var pickUpState = config.Create();
                gameState.PickUps.Add(pickUpState);
                var pickUpViewModel = new PickUpViewModel(pickUpState);
                pickUp.Bind(pickUpViewModel);
            }
        }
    }
}