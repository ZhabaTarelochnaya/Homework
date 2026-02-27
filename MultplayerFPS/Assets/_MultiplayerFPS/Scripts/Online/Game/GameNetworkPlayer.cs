using System;
using _MultiplayerFPS.Scripts.Components;
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
        [SerializeField] Weapon _weapon;
        [SerializeField] Transform _cameraTarget;
        
        public override void OnStartLocalPlayer()
        {
            var inputService = new MouseKeyboardInputService();
            ServiceLocator.Current.Register<IInputService>(inputService);
            var moveService = new CharacterControllerPlayerMovementService(_characterController);
            ServiceLocator.Current.Register<IPlayerMovementService>(moveService);
            var cameraManager = new CameraManager(Camera.main);
            ServiceLocator.Current.Register<ICameraManager>(cameraManager);
            
            _playerController = new PlayerController();
            _gameNetworkPlayerView.Init();
            _weapon.Init();
        }
        public override void OnStopLocalPlayer()
        {
            ServiceLocator.Current.Unregister<IInputService>();
            ServiceLocator.Current.Unregister<IPlayerMovementService>();
            ServiceLocator.Current.Unregister<ICameraManager>();
        }
        void Update()
        {
            if (isLocalPlayer)
            {
                _playerController.HandleMove();
                _playerController.HandleJump();
                _playerController.HandlePlayerRotation(transform);
                _playerController.HandleShoot(_weapon);
                
                _gameNetworkPlayerView.HandleAnimations();
            }
        }
        void LateUpdate()
        {
            if (isLocalPlayer)
            {
                _playerController.HandleCameraRotation(_cameraTarget, transform);
            }
        }
    }
}
