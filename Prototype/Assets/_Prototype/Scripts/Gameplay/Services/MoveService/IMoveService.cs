using _Prototype.Scripts.Utils.ServiceLocator;
using UnityEngine;

namespace _Prototype.Scripts.Gameplay.Services.MoveService
{
    public interface IMoveService : IService
    {
        public void Move(Rigidbody2D rigidbody, Vector2 direction, float speed);
    }
}