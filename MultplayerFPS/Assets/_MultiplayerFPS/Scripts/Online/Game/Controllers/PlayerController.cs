using _MultiplayerFPS.Scripts.Config;
using _MultiplayerFPS.Scripts.Services.Config;
using _MultiplayerFPS.Scripts.Services.InputService;
using _MultiplayerFPS.Scripts.Services.Move;
using _MultiplayerFPS.Scripts.Utils.ServiceLocator;
using UnityEngine;

namespace _MultiplayerFPS.Scripts.Controllers
{
    public class PlayerController
    {
        readonly IInputService _inputService;
        readonly IPlayerMovementService _playerMovementService;
        readonly CameraManager _cameraManager;
        
        public PlayerController()
        {
            _inputService = ServiceLocator.Current.Get<IInputService>();
            _playerMovementService = ServiceLocator.Current.Get<IPlayerMovementService>();
            _cameraManager = new CameraManager(Camera.main);
        }

        public void HandleJump()
        {
            if (_inputService.JumpButtonDown())
            {
                _playerMovementService.AddJump();
            }
        }
        public void HandleMove()
        {
            var input = _inputService.GetMove();
            
            _playerMovementService.AddRun(input);
            _playerMovementService.Move();
        }

        public void HandlePlayerRotation(Transform player)
        {
            var look = _inputService.GetLook();
            
            player.Rotate(Vector3.up * look.x);
        }
        public void HandleCameraRotation(Transform head, Transform player)
        {
            _cameraManager.FollowPosition(head);
            
            var look = _inputService.GetLook();
            _cameraManager.FollowRotation(look.y, player.rotation.eulerAngles.y);
        }
    }
}