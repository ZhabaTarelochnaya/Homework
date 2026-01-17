using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace.Gameplay.Data.PickUp;
using DefaultNamespace.Gameplay.View.Enemies.Types;
using UnityEngine;

namespace DefaultNamespace.Gameplay.View.PickUp
{
    public class EnemySpawnerView : MonoBehaviour
    {
        EnemySpawnerViewModel _viewModel;
        [SerializeField] bool _showBounds;
        [SerializeField] Vector2 _bounds;
        public void Bind(EnemySpawnerViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        void Start()
        {
            StartCoroutine(_viewModel.StartSpawning(transform, _bounds));
        }
        
        void OnDrawGizmos()
        {
            if (!_showBounds) return;
            Vector2 center = transform.position;
            Vector2 halfBounds = new Vector2(_bounds.x / 2f, _bounds.y / 2f);
            
            Vector2 p1 = center + new Vector2(-halfBounds.x, -halfBounds.y);
            Vector2 p2 = center + new Vector2(halfBounds.x, -halfBounds.y);
            Vector2 p3 = center + new Vector2(halfBounds.x, halfBounds.y);
            Vector2 p4 = center + new Vector2(-halfBounds.x, halfBounds.y);
            
            Gizmos.color = new Color(1, 0, 0, 0.3f); 
            Gizmos.DrawCube(center, new Vector3(_bounds.x, _bounds.y, 0));
            
            Gizmos.color = Color.red;
            Gizmos.DrawLine(p1, p2);
            Gizmos.DrawLine(p2, p3);
            Gizmos.DrawLine(p3, p4);
            Gizmos.DrawLine(p4, p1);
        }
    }
}