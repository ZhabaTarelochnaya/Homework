using DefaultNamespace.Gameplay.Controllers;
using DefaultNamespace.Gameplay.Data;
using DefaultNamespace.Gameplay.Data.Enemy;
using Gameplay.View.World.Enemies.HitHurtBoxes;
using UnityEngine;

namespace DefaultNamespace.Gameplay.View.Enemies.Types
{
    public class EnemyViewModel
    {
        readonly EnemyState _enemy;
        readonly EnemyController _enemyController;
        public int ID => _enemy.ID;
        public EnemyType Type => _enemy.Type;
        public Vector2 Velocity => _enemy.Velocity;
        public Vector2 Position { get => _enemy.Position; set => _enemy.Position = value; }
        public HitBoxViewModel HitBoxViewModel { get; }

        public EnemyViewModel(EnemyState enemy, EnemyController enemyController)
        {
            _enemy = enemy;
            _enemyController = enemyController;
            HitBoxViewModel = new HitBoxViewModel(enemy);
        }
        public void FixedUpdate() => _enemyController.FixedUpdate();
    }
}