using System;
using DefaultNamespace.Gameplay.Data;
using UnityEngine;

namespace DefaultNamespace.Gameplay.World.Player
{
    public class PlayerViewModel
    {
        readonly PlayerState _playerState;
        readonly PlayerController _playerController;
        public Vector3 Velocity { get => _playerState.Velocity; set => _playerState.Velocity = value; }
        public Vector3 Position { get => _playerState.Position; set => _playerState.Position = value; }
        public Vector3 Direction { get => _playerState.InputMoveDirection; set => _playerState.InputMoveDirection = value; }
        public Quaternion CameraRotation { get => _playerState.CameraRotation; set => _playerState.CameraRotation = value; }
        public float MouseSensitivity { get => _playerState.MouseSensitivity; }

        public PlayerViewModel(PlayerState playerState, PlayerController playerController)
        {
            _playerState = playerState;
            _playerController = playerController;
        }
        public void FixedUpdate() => _playerController.FixedUpdate();
    }
}