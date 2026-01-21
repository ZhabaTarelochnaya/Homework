using System;
using _TopDownShooter.Scripts.Gameplay.Configs.Enemies;
using _TopDownShooter.Scripts.Gameplay.Services;
using _TopDownShooter.Scripts.Utils.ServiceLocator;
using UnityEngine;
using UnityEngine.AI;
using Object = UnityEngine.Object;

namespace _TopDownShooter.Scripts.View
{
    public class EnemyController : IDisposable
    {
        readonly NavMeshAgent _agent;
        readonly HurtBox _hurtBox;
        readonly Transform _target;
        readonly EnemyConfig _config;
        readonly MoveService _moveService;

        public EnemyController(NavMeshAgent agent, HurtBox hurtBox, HitBox hitBox,
            Transform target, EnemyConfig config)
        {
            _agent = agent;
            _hurtBox = hurtBox;
            _target = target;
            _config = config;
            _moveService = ServiceLocator.Current.Get<MoveService>();

            _agent.speed = _config.Speed;
            hitBox.Damage = _config.Damage;
            _hurtBox.MaxHealth = _config.MaxHealth;
            _hurtBox.HealFullHealth();
            _hurtBox.Died += HurtBoxOnDied;
        }

        void HurtBoxOnDied()
        {
            Object.Destroy(_agent.gameObject);
        }
        
        public void FixedUpdate()
        {
            _moveService.MoveToTarget(_agent, _target.position);
        }

        public void Dispose()
        {
            _hurtBox.Died -= HurtBoxOnDied;
        }
    }
}