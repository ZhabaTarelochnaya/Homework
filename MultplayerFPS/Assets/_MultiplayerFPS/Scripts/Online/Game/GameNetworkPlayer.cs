using System;
using System.Collections.Generic;
using _MultiplayerFPS.Scripts.Components;
using _MultiplayerFPS.Scripts.Controllers;
using _MultiplayerFPS.Scripts.Services.InputService;
using _MultiplayerFPS.Scripts.Services.Move;
using _MultiplayerFPS.Scripts.Services.State;
using _MultiplayerFPS.Scripts.State;
using _MultiplayerFPS.Scripts.Utils.ServiceLocator;
using Mirror;
using UnityEngine;

namespace _MultiplayerFPS.Scripts
{
    public class GameNetworkPlayer : NetworkBehaviour
    {
        IInputService _inputService;
        PlayerController _playerController;
        string _lobbyPlayerNickname;
        Color _lobbyPlayerColor;
        [SerializeField] CharacterController _characterController;
        [SerializeField] GameNetworkPlayerView _gameNetworkPlayerView;
        [SerializeField] Weapon _weapon;
        [SerializeField] Transform _cameraTarget;
        [SerializeField] PlayerState _playerState;
        

        public override void OnStartClient()
        {
            if (isLocalPlayer)
            {
                var inputService = new MouseKeyboardInputService();
                ServiceLocator.Current.Register<IInputService>(inputService);
                var moveService = new CharacterControllerPlayerMovementService(_characterController);
                ServiceLocator.Current.Register<IPlayerMovementService>(moveService);
                var cameraManager = new CameraManager(Camera.main);
                ServiceLocator.Current.Register<ICameraManager>(cameraManager);
                
                _playerController = new PlayerController(_weapon, _cameraTarget, transform);
            }
        }
        public override void OnStartServer()
        {
            var stateService = ServiceLocator.Current.Get<IStateService>();
            stateService.GameState.PlayerStates.TryAdd(netId, _playerState);
            _playerState.Nickname = _lobbyPlayerNickname;
            _playerState.Color = _lobbyPlayerColor;
        }

        public override void OnStopLocalPlayer()
        {
            ServiceLocator.Current.Unregister<IInputService>();
            ServiceLocator.Current.Unregister<IPlayerMovementService>();
            ServiceLocator.Current.Unregister<ICameraManager>();
        }
        [Server]
        public void Init(string nickname, Color color)
        {
            _lobbyPlayerNickname = nickname;
            _lobbyPlayerColor = color;
        }
        void Update()
        {
            if (isLocalPlayer)
            {
                _playerController.Update();
                _gameNetworkPlayerView.HandleAnimations();
                
            }
        }
        void LateUpdate()
        {
            if (isLocalPlayer)
            {
                _playerController.UpdateCamera();
            }
        }
    }
}
