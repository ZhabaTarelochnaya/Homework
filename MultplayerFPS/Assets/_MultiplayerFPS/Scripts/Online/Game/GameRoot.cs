using System;
using _MultiplayerFPS.Scripts.Services.LoggerService;
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
        HUDPresenter _hudPresenter;
        [SerializeField] HUDView _hudPrefab;
        [SerializeField] GameState _gameState;

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
            
            var hudView = Instantiate(_hudPrefab);
            _hudPresenter = new HUDPresenter(hudView);
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

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                foreach (var pair in _gameState.PlayerStates)
                {
                    Debug.Log(pair.Key);
                    Debug.Log(pair.Value);
                }
            }
        }
    }
}