using UnityEngine;

namespace DefaultNamespace.Gameplay.World
{
    public interface IMovable
    {
        public float Speed { get; set; }
        public Vector3 Position { get; set; }
        public Vector3 Velocity { get; set; }
    }
}