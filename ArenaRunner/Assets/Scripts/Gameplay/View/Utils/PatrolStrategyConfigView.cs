using System;
using DefaultNamespace.Gameplay.Data.Strategies;
using UnityEngine;

namespace Gameplay.View.World.Enemies
{
    public class PatrolStrategyConfigView : MonoBehaviour
    {
        [SerializeField] PatrolStrategyConfig _config;

        void OnDrawGizmos()
        {
            if (!_config) return;
            Vector2 center = _config.PatrolBoundsCenter;
            Vector2 halfBounds = new Vector2(_config.PatrolBounds.x / 2f, _config.PatrolBounds.y / 2f);
            
            Vector2 p1 = center + new Vector2(-halfBounds.x, -halfBounds.y);
            Vector2 p2 = center + new Vector2(halfBounds.x, -halfBounds.y);
            Vector2 p3 = center + new Vector2(halfBounds.x, halfBounds.y);
            Vector2 p4 = center + new Vector2(-halfBounds.x, halfBounds.y);
            
            Gizmos.color = new Color(1, 0, 0, 0.3f); 
            Gizmos.DrawCube(center, new Vector3(_config.PatrolBounds.x, _config.PatrolBounds.y, 0));
            
            Gizmos.color = Color.red;
            Gizmos.DrawLine(p1, p2);
            Gizmos.DrawLine(p2, p3);
            Gizmos.DrawLine(p3, p4);
            Gizmos.DrawLine(p4, p1);
        }
    }
}