using System;
using DefaultNamespace.Gameplay.World;
using UnityEngine;

namespace DefaultNamespace.Gameplay.Data
{
    public class PlayerState : IMovable
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
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public Vector2 InputMoveDirection { get; set; }
        
        public event Action Died;

        public PlayerState(PlayerData playerData)
        {
            Speed = playerData.Speed;
            IsDead = playerData.IsDead;
            Position = playerData.Position;
        }

        public PlayerData ToData()
        {
            var playerData = new PlayerData();
            playerData.Speed = Speed;
            playerData.IsDead = IsDead;
            playerData.Position = Position;
            return playerData;
        }
    }
}