using System;
using _MultiplayerFPS.Scripts.Controllers;
using _MultiplayerFPS.Scripts.Services.InputService;
using _MultiplayerFPS.Scripts.Services.Move;
using _MultiplayerFPS.Scripts.Utils.ServiceLocator;
using Mirror;
using UnityEngine;

namespace _MultiplayerFPS.Scripts
{
    [DefaultExecutionOrder(-100)]
    public class GameNetworkPlayer : NetworkBehaviour
    {
        IInputService _inputService;
        PlayerController _playerController;
        [SerializeField] CharacterController _characterController;
        [SerializeField] GameNetworkPlayerView _gameNetworkPlayerView;
        
        public override void OnStartLocalPlayer()
        {
            var inputService = new MouseKeyboardInputService();
            ServiceLocator.Current.Register<IInputService>(inputService);
            var moveService = new CharacterControllerPlayerMovementService(_characterController);
            ServiceLocator.Current.Register<IPlayerMovementService>(moveService);

            _playerController = new PlayerController();
            _gameNetworkPlayerView.Init(_playerController);
        }
        public override void OnStopLocalPlayer()
        {
            ServiceLocator.Current.Unregister<IInputService>();
            ServiceLocator.Current.Unregister<IPlayerMovementService>();
        }

        void Update()
        {
            if (isLocalPlayer)
            {
                _playerController.HandleMove();
                _playerController.HandleJump();
                _playerController.HandlePlayerRotation(transform);
            }
        }
    }
}
