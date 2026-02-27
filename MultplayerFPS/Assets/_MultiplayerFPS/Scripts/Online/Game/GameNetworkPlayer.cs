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
        IWeaponView _view;
        float _timer;
        PlayerController _playerController;
        [SerializeField] CharacterController _characterController;
        [SerializeField] GameNetworkPlayerView _gameNetworkPlayerView;
        [SerializeField] Weapon _weapon;
        [SerializeField] GameObject _weaponView;
        
        public override void OnStartLocalPlayer()
        {
            var inputService = new MouseKeyboardInputService();
            ServiceLocator.Current.Register<IInputService>(inputService);
            var moveService = new CharacterControllerPlayerMovementService(_characterController);
            ServiceLocator.Current.Register<IPlayerMovementService>(moveService);
            _inputService = inputService;
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
                
                if (_inputService.GetShootButton())
                {
                    // var shootPos = _weapon.ShootSource.position;
                    // var shootDirection = _weapon.ShootSource.forward;
                    // _weapon.CmdShoot(shootPos, shootDirection);
                    Camera cam = Camera.main;

                    Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

                    Vector3 targetPoint;

                    if (Physics.Raycast(ray, out RaycastHit hit, _weapon.Config.Range))
                    {
                        targetPoint = hit.point;
                    }
                    else
                    {
                        targetPoint = ray.origin + ray.direction * _weapon.Config.Range;
                    }
                    Vector3 shootOrigin = _weapon.ShootSource.position;
                    Vector3 shootDirection = (targetPoint - shootOrigin).normalized;
                    _weapon.CmdShoot(shootOrigin, shootDirection);
                }
                
            }
        }
    }
}
