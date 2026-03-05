using _MultiplayerFPS.Scripts.Config;
using _MultiplayerFPS.Scripts.Services.Config;
using _MultiplayerFPS.Scripts.Services.State;
using _MultiplayerFPS.Scripts.State;
using _MultiplayerFPS.Scripts.Utils.ServiceLocator;
using UnityEngine;

namespace _MultiplayerFPS.Scripts.Services.Move
{
    public class CharacterControllerPlayerMovementService : IPlayerMovementService
    {
        readonly CharacterController _characterController;
        
        Vector3 _movement;
        float _verticalVelocity;
        PlayerConfig _playerConfig;
        PlayerState _playerState;
        
        public Vector3 Velocity { get; private set; }
        
        public CharacterControllerPlayerMovementService(CharacterController characterController, PlayerState playerState)
        {
            _characterController = characterController;
            _playerState = playerState;
            _playerConfig = ServiceLocator.Current.Get<IConfigService>().Get<PlayerConfig>();
        }
        
        public void AddRun(Vector2 direction)
        {
            direction.Normalize();
            Vector3 localDirection = new Vector3(direction.x, 0f, direction.y);
            Vector3 worldDirection = _characterController.transform.TransformDirection(localDirection);
            _movement += worldDirection * _playerState.Speed;
        }
        public void AddJump()
        {
            if (!_characterController.isGrounded) return;
            _verticalVelocity = Mathf.Sqrt( -2f * _playerConfig.Gravity * _playerState.JumpHeight);
        }
        public void Move()
        {
            HandleGravity();
            Velocity = _movement;
            _characterController.Move(_movement * Time.deltaTime);
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
                _verticalVelocity += _playerConfig.Gravity * Time.deltaTime;
            }
            _movement.y = _verticalVelocity;
        }
    }
}