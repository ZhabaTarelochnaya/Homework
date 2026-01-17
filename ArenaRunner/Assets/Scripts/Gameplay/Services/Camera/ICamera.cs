using UnityEngine;

namespace Gameplay.Services.Camera
{
    public interface ICamera
    {
        public Vector2 Position { get; set; }
    }
}