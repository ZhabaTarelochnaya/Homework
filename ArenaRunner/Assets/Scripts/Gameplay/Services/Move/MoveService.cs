using UnityEngine;
using Utils.ServiceLocator;

namespace DefaultNamespace.Gameplay.World
{
    public class MoveService : IService
    {
        public void Move(
            IMovable movable, 
            Vector3 direction, 
            float deltaTime)
        {
            if (movable == null) return;
            movable.Velocity = direction * movable.Speed;
            movable.Position += movable.Velocity * deltaTime;
        }
        
        public void MoveTowards(
            IMovable movable,
            Vector3 targetPosition,
            float deltaTime,
            float stoppingDistance = 0.1f)
        {
            if (movable == null) return;
            Vector3 toTarget = targetPosition - movable.Position;
            float distance = (targetPosition - movable.Position).magnitude;
            
            if (distance <= stoppingDistance)
            {
                movable.Velocity = Vector3.zero;
                return;
            }

            movable.Velocity = toTarget.normalized * movable.Speed;
            movable.Position += movable.Velocity * deltaTime;
        }
        
        public void Teleport(IMovable movable, Vector3 position)
        {
            if (movable == null) return;
            movable.Position = position;
            movable.Velocity = Vector3.zero;
        }
    }
}