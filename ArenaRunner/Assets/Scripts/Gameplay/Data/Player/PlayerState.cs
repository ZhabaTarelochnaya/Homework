using System;
using DefaultNamespace.Gameplay.World;
using Gameplay.View.World.Enemies.HitHurtBoxes;
using UnityEngine;

namespace DefaultNamespace.Gameplay.Data
{
    public class PlayerState : IMovable, IHealthUser
    {
        int _currentHealth;
        public float Speed { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public Vector2 InputMoveDirection { get; set; }

        public int MaxHealth { get; set; }

        public int CurrentHealth
        {
            get => _currentHealth;
            set
            {
                _currentHealth = value;
                PlayerDamaged?.Invoke(value);
                if (_currentHealth <= 0)
                {
                    Died?.Invoke();
                }
            }
        }

        public event Action Died;
        public event Action<int> PlayerDamaged; 
        public PlayerState(PlayerData playerData)
        {
            Speed = playerData.Speed;
            Position = playerData.Position;
            CurrentHealth = playerData.CurrentHealth;
            MaxHealth = playerData.MaxHealth;
        }

        public PlayerData ToData()
        {
            var playerData = new PlayerData();
            playerData.Speed = Speed;
            playerData.Position = Position;
            CurrentHealth = playerData.CurrentHealth;
            MaxHealth = playerData.MaxHealth;
            return playerData;
        }
    }
}