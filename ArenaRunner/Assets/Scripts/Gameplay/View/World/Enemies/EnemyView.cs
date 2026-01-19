using System;
using DefaultNamespace.Gameplay.Data;
using Gameplay.View.World.Enemies.HitHurtBoxes;
using UnityEngine;

namespace DefaultNamespace.Gameplay.View.Enemies.Types
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class EnemyView : MonoBehaviour
    {
        Rigidbody2D _rb;
        EnemyViewModel _enemyViewModel;
        [SerializeField] HitBoxView _hitBoxView;
        public int ID => _enemyViewModel.ID;
        public EnemyType Type => _enemyViewModel.Type;

        void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }
        public void Bind(EnemyViewModel enemyViewModel)
        {
            _enemyViewModel = enemyViewModel;
            _hitBoxView.Bind(enemyViewModel.HitBoxViewModel);
        }
        void FixedUpdate()
        {
            _enemyViewModel.FixedUpdate();
            
            _rb.velocity = _enemyViewModel.Velocity;
            
            _enemyViewModel.Position = _rb.position;
        }
    }
}