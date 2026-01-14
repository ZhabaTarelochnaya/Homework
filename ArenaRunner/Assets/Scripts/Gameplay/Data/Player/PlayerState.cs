using System;
using DefaultNamespace.Gameplay.Services;
using DefaultNamespace.Gameplay.World;
using UnityEngine;

namespace DefaultNamespace.Gameplay.Data
{
    public class PlayerState : IMovable, ICameraUser
    {
        bool _isDead = false;
        public float Speed { get; set; }

        public bool IsDead
        {
            get => _isDead;
            set
            {
                _isDead = value;
                if (_isDead) Died?.Invoke();
            }
        }
        public Vector3 Position { get; set; }
        public Vector3 Velocity { get; set; }
        public Vector3 InputMoveDirection { get; set; }

        public Quaternion CameraRotation { get; set; }
        public float MouseSensitivity { get; set; }
        
        public event Action Died;

        public PlayerState(PlayerData playerData)
        {
            Speed = playerData.Speed;
            IsDead = playerData.IsDead;
            Position = playerData.Position;
            CameraRotation = Quaternion.Euler(playerData.CameraRotation);
            MouseSensitivity = playerData.MouseSensitivity;
        }

        public PlayerData ToData()
        {
            var playerData = new PlayerData();
            playerData.Speed = Speed;
            playerData.IsDead = IsDead;
            playerData.Position = Position;
            playerData.CameraRotation = CameraRotation.eulerAngles;
            playerData.MouseSensitivity = MouseSensitivity;
            return playerData;
        }
    }
}