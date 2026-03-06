using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _MultiplayerFPS.Scripts.Components;
using _MultiplayerFPS.Scripts.Config;
using _MultiplayerFPS.Scripts.Services;
using _MultiplayerFPS.Scripts.Services.Config;
using _MultiplayerFPS.Scripts.Services.GrenadeService;
using _MultiplayerFPS.Scripts.Services.LoggerService;
using _MultiplayerFPS.Scripts.Services.MatchTime;
using _MultiplayerFPS.Scripts.Services.Respawn;
using _MultiplayerFPS.Scripts.Services.State;
using _MultiplayerFPS.Scripts.State;
using _MultiplayerFPS.Scripts.States;
using _MultiplayerFPS.Scripts.Utils.FiniteStateMachine;
using _MultiplayerFPS.Scripts.Utils.ServiceLocator;
using Mirror;
using UnityEngine;

namespace _MultiplayerFPS.Scripts
{
    [DefaultExecutionOrder(-100)]
    public class GameRoot : NetworkBehaviour
    {
        ILoggerService _loggerService;
        IPickupService _pickupService;
        IStateService _stateService;
        FSM<GameStateName> _gameStateFsm = new ();
        [SerializeField] GameState _gameState;
        [SerializeField] Transform[] _spawnPoints;
        [SerializeField] Transform[] _pickUpSpawnPoints;
        
        public override void OnStartServer()
        {
            
            _stateService = new StateService(_gameState);
            ServiceLocator.Current.Register(_stateService);
            var respawnService = new RandomRespawnService(_spawnPoints);
            ServiceLocator.Current.Register<IRespawnService>(respawnService);
            var grenadeService = new GrenadeService();
            ServiceLocator.Current.Register<IGrenadeService>(grenadeService);
            var playerScoreService = new PlayerScoreService(_stateService);
            ServiceLocator.Current.Register<IPlayerScoreService>(playerScoreService);
            var matchTimeService = new MatchTimeService(_gameState);
            ServiceLocator.Current.Register<IMatchTimeService>(matchTimeService);
            _pickupService = new PickupService(_pickUpSpawnPoints, _stateService);
            ServiceLocator.Current.Register(_pickupService);
            
            _gameState.GameStateChanged += GameStateOnGameStateChanged;
            
            _gameStateFsm.AddState(new InitState(_gameState))
                .AddState(new MatchGoingState(_gameState, matchTimeService))
                .AddState(new MatchEndedState(_gameState, SetEnableCursor));
        }
        public override void OnStopServer()
        {
            ServiceLocator.Current.Unregister<IStateService>();
            ServiceLocator.Current.Unregister<IPickupService>();
            ServiceLocator.Current.Unregister<IGrenadeService>();
            ServiceLocator.Current.Unregister<IPlayerScoreService>();
            ServiceLocator.Current.Unregister<IMatchTimeService>();
            
        }

        public override void OnStartClient()
        {
            if (!isServer)
            {
                _stateService = new StateService(_gameState);
                ServiceLocator.Current.Register(_stateService);
                var respawnService = new RandomRespawnService(_spawnPoints);
                ServiceLocator.Current.Register<IRespawnService>(respawnService);
            }
            Cursor.lockState = CursorLockMode.Locked;
        }

        public override void OnStopClient()
        {
            Cursor.lockState = CursorLockMode.None;
            ServiceLocator.Current.Unregister<IStateService>();
            ServiceLocator.Current.Unregister<IRespawnService>();
        }
        void Update()
        {
            if (!isServer) return;
            _gameStateFsm.Tick(Time.deltaTime);
        }
        void GameStateOnGameStateChanged(GameStateName obj)
        {
            if (obj != GameStateName.Unregister) return;
            RpcUnregister();
        }
        [ClientRpc]
        void RpcUnregister()
        {
            if (!isServer)
            {
                ServiceLocator.Current.Unregister<IStateService>();
            }
            ServiceLocator.Current.Unregister<IRespawnService>();
            _gameState.GameStateChanged -= GameStateOnGameStateChanged;
        }
        [ClientRpc]
        void SetEnableCursor() => Cursor.lockState = CursorLockMode.None;
    }
}