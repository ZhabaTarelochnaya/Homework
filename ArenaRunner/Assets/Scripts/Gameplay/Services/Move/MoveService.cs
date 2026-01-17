using UnityEngine;
using Utils.ServiceLocator;

namespace DefaultNamespace.Gameplay.World
{
    public class MoveService : IService
    {
        public void Move(
            IMovable movable, 
            Vector2 direction, 
            float deltaTime)
        {
            movable.Velocity = direction * movable.Speed;
            movable.Position += movable.Velocity * deltaTime;
        }
        
        public void MoveTowards(
            IMovable movable,
            Vector2 targetPosition,
            float deltaTime,
            float stoppingDistance = 0.1f)
        {
            Vector2 toTarget = targetPosition - movable.Position;
            float distance = (targetPosition - movable.Position).magnitude;
            
            if (distance <= stoppingDistance)
            {
                movable.Velocity = Vector2.zero;
                return;
            }

            movable.Velocity = toTarget.normalized * movable.Speed;
            movable.Position += movable.Velocity * deltaTime;
        }
        
        public void Teleport(IPositionUser positionUser, Vector2 position)
        {
            positionUser.Position = position;
        }
    }
}