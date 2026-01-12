using System;
using DefaultNamespace.Gameplay.Services;
using DefaultNamespace.Gameplay.World;
using UnityEngine;

namespace DefaultNamespace.Gameplay.Data
{
    public class PlayerDataProxy : IMovable, ICameraUser
    {
        readonly PlayerData _playerData;
        
        public float Speed { get => _playerData.Speed; set => _playerData.Speed = value; }

        public bool IsDead
        {
            get => _playerData.IsDead;
            set
            {
                _playerData.IsDead = value;
                if (_playerData.IsDead) Died?.Invoke();
            }
        }
        public Vector3 Position { get => _playerData.Position; set => _playerData.Position = value; }
        public Vector3 Velocity { get; set; }
        public Vector3 Direction { get; set; }

        public Vector3 CameraRotation { get => _playerData.CameraRotation; 
                                        set => _playerData.CameraRotation = value; }
        public float MouseSensitivity { get => _playerData.MouseSensitivity; 
                                        set => _playerData.MouseSensitivity = value; }

        public event Action Died;

        public PlayerDataProxy(PlayerData playerData)
        {
            _playerData = playerData;
        }
    }
}