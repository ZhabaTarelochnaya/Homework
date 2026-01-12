using System;
using DefaultNamespace.Gameplay.Data;
using UnityEngine;

namespace DefaultNamespace.Gameplay.World.Player
{
    public class PlayerViewModel
    {
        readonly PlayerDataProxy _playerDataProxy;
        readonly PlayerController _playerController;
        public Vector3 Velocity { get => _playerDataProxy.Velocity; set => _playerDataProxy.Velocity = value; }
        public Vector3 Position { get => _playerDataProxy.Position; set => _playerDataProxy.Position = value; }
        public Vector3 Direction { get => _playerDataProxy.Direction; set => _playerDataProxy.Direction = value; }
        public Vector3 CameraRotation { get => _playerDataProxy.CameraRotation; set => _playerDataProxy.CameraRotation = value; }
        public float MouseSensitivity { get => _playerDataProxy.MouseSensitivity; }

        public PlayerViewModel(PlayerDataProxy playerDataProxy, PlayerController playerController)
        {
            _playerDataProxy = playerDataProxy;
            _playerController = playerController;
        }
        public void FixedUpdate() => _playerController.FixedUpdate();
    }
}