using _MultiplayerFPS.Scripts.Components;
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
        readonly ICameraManager _cameraManager;
        
        public PlayerController()
        {
            _inputService = ServiceLocator.Current.Get<IInputService>();
            _playerMovementService = ServiceLocator.Current.Get<IPlayerMovementService>();
            _cameraManager = ServiceLocator.Current.Get<ICameraManager>();
        }

        public void HandleJump()
        {
            if (_inputService.GetJumpButtonDown())
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
        public void HandleShoot(Weapon weapon)
        {
            if (_inputService.GetShootButton())
            {
                Camera cam = _cameraManager.CurrentCamera;
                Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
                Vector3 targetPoint;
                if (Physics.Raycast(ray, out RaycastHit hit, weapon.Config.Range))
                {
                    targetPoint = hit.point;
                }
                else
                {
                    targetPoint = ray.origin + ray.direction * weapon.Config.Range;
                }
                Vector3 shootOrigin = weapon.ShootOrigin.position;
                Vector3 shootDirection = (targetPoint - shootOrigin).normalized;
                
                weapon.CmdShoot(shootOrigin, shootDirection);
            }
        }
    }
}