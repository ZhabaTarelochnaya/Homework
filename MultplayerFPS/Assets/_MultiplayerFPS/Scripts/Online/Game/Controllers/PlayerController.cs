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
        readonly IMovementService _movementService;
        
        public PlayerController()
        {
            _inputService = ServiceLocator.Current.Get<IInputService>();
            _movementService = ServiceLocator.Current.Get<IMovementService>();
        }

        public void HandleJump()
        {
            if (_inputService.JumpButtonDown())
            {
                _movementService.AddJump();
            }
        }
        public void HandleMove()
        {
            var input = _inputService.GetMove();
            _movementService.AddRun(input);
            _movementService.Move();
        }
    }
}