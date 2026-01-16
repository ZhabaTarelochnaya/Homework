using System;
using DefaultNamespace.Gameplay.Data;
using UnityEngine;

namespace DefaultNamespace.Gameplay.View.Enemies.Types
{
    [RequireComponent(typeof(Rigidbody))]
    public class EnemyView : MonoBehaviour
    {
        Rigidbody _rb;
        EnemyViewModel _enemyViewModel;
        public int ID => _enemyViewModel.ID;
        public EnemyType Type => _enemyViewModel.Type;

        void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }
        public void Bind(EnemyViewModel enemyViewModel)
        {
            _enemyViewModel = enemyViewModel;
        }
        void FixedUpdate()
        {
            _enemyViewModel.FixedUpdate();
            
            _rb.velocity = _enemyViewModel.Velocity;
            
            _enemyViewModel.Position = _rb.position;
        }
    }
}