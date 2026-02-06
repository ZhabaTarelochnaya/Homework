using UnityEngine;

namespace _Prototype.Scripts.Gameplay.Services.MoveService
{
    public class MoveService : IMoveService
    {
        public void Move(Rigidbody2D rigidbody, Vector2 direction, float speed)
        {
            rigidbody.velocity = direction * speed;
        }
    }
}