using UnityEngine;

namespace DefaultNamespace.Gameplay.World
{
    public interface IMovable : IPositionUser
    {
        public float Speed { get; set; }
        public Vector2 Velocity { get; set; }
    }
}