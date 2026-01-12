using System;
using DefaultNamespace.Gameplay.World;
using UnityEngine;

namespace DefaultNamespace.Gameplay.Data
{
    public class PlayerDataProxy : IMovable
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
        public Vector3 Rotation { get => _playerData.Rotation; set => _playerData.Rotation = value; }
        public Vector3 Velocity { get; set; }
        public Vector3 Direction { get; set; }
        public event Action Died;

        public PlayerDataProxy(PlayerData playerData)
        {
            _playerData = playerData;
        }
    }
}