using _MultiplayerFPS.Scripts.Config;
using _MultiplayerFPS.Scripts.Services.Config;
using _MultiplayerFPS.Scripts.Utils.ServiceLocator;
using UnityEngine;

namespace _MultiplayerFPS.Scripts.Services.Move
{
    public class CharacterControllerMovementService : IMovementService
    {
        readonly CharacterController _characterController;
        
        Vector3 _movement;
        float _verticalVelocity;
        PlayerConfig _playerConfig;

        public CharacterControllerMovementService(CharacterController characterController)
        {
            _characterController = characterController;
            _playerConfig = ServiceLocator.Current.Get<IConfigService>().Get<PlayerConfig>();
        }
        
        public void AddRun(Vector2 direction)
        {
            direction.Normalize();
            var velocity = new Vector3(direction.x, 0, direction.y) * _playerConfig.Speed;
            _movement += velocity;
        }
        public void AddJump()
        {
            if (!_characterController.isGrounded) return;
            _verticalVelocity = Mathf.Sqrt( -2f * _playerConfig.Gravity * _playerConfig.JumpHeight);
        }
        public void Move()
        {
            HandleGravity();
            _characterController.Move(_movement * Time.fixedDeltaTime);
            _movement = Vector3.zero;
        }
        void HandleGravity()
        {
            if (_characterController.isGrounded && _verticalVelocity <= 0)
            {
                _verticalVelocity = -2f;
            }
            else
            {
                _verticalVelocity += _playerConfig.Gravity * Time.fixedDeltaTime;
            }
            _movement.y = _verticalVelocity;
            
            Debug.Log(_movement.y);
        }
    }
}