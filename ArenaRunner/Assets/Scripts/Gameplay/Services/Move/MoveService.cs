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
            movable.Velocity = direction * movable.Speed;
            movable.Position += movable.Velocity * deltaTime;
        }
        
        public void MoveTowards(
            IMovable movable,
            Vector3 targetPosition,
            float deltaTime,
            float stoppingDistance = 0.1f)
        {
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
        
        public void Teleport(IPositionUser positionUser, Vector3 position)
        {
            positionUser.Position = position;
        }

        public Vector3 RotateAroundYAxis(Vector3 dir, float angleDeg)
        {
            float a = -angleDeg * Mathf.Deg2Rad;
            return new Vector3(
                dir.x * Mathf.Cos(a) - dir.z * Mathf.Sin(a),
                dir.y,
                dir.x * Mathf.Sin(a) + dir.z * Mathf.Cos(a)
            );
        }
    }
}