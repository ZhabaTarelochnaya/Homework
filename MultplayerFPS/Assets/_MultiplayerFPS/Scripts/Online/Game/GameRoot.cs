using System;
using _MultiplayerFPS.Scripts.Services.LoggerService;
using _MultiplayerFPS.Scripts.Services.Respawn;
using _MultiplayerFPS.Scripts.Services.State;
using _MultiplayerFPS.Scripts.State;
using _MultiplayerFPS.Scripts.Utils.ServiceLocator;
using Mirror;
using UnityEngine;

namespace _MultiplayerFPS.Scripts
{
    public class GameRoot : NetworkBehaviour
    {
        ILoggerService _loggerService;
        [SerializeField] GameState _gameState;
        [SerializeField] Transform[] _spawnPoints;

        public override void OnStartServer()
        {
            var stateService = new StateService(_gameState);
            ServiceLocator.Current.Register<IStateService>(stateService);
        }
        public override void OnStopServer()
        {
            ServiceLocator.Current.Unregister<IStateService>();
        }
        public override void OnStartClient()
        {
            if (!isServer)
            {
                var stateService = new StateService(_gameState);
                ServiceLocator.Current.Register<IStateService>(stateService);
            }
            var respawnService = new RandomRespawnService(_spawnPoints);
            ServiceLocator.Current.Register<IRespawnService>(respawnService);
            
            Cursor.lockState = CursorLockMode.Locked;
            _loggerService = ServiceLocator.Current.Get<ILoggerService>();
            _loggerService.Log("Match started");
        }
        public override void OnStopClient()
        {
            if (!isServer)
            {
                ServiceLocator.Current.Unregister<IStateService>();
            }
        }
    }
}