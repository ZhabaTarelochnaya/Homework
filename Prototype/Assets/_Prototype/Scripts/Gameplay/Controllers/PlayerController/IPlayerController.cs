using UnityEngine;

namespace _Prototype.Scripts.Gameplay.Controllers
{
    public interface IPlayerController
    {
        public void FixedUpdate(Rigidbody2D rigidbody);
    }
}