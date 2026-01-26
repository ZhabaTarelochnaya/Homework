using System;
using _TopDownShooter.Scripts.Gameplay.Configs.Enemies;
using _TopDownShooter.Scripts.Gameplay.Services;
using _TopDownShooter.Scripts.Utils.EventBus;
using _TopDownShooter.Scripts.Utils.ServiceLocator;
using UnityEngine;
using UnityEngine.AI;
using Object = UnityEngine.Object;

namespace _TopDownShooter.Scripts.View
{
    public class EnemyController
    {
        readonly NavMeshAgent _agent;
        readonly HurtBox _hurtBox;
        readonly EnemyView _enemyView;
        readonly Transform _target;
        readonly Action<EnemyView> _destroy;
        readonly EnemyConfig _config;
        readonly MoveService _moveService;
        readonly EventBus _eventBus;

        public float AIPathfindingFrequency { get; private set; }

        public EnemyController(EnemyView enemyView, EnemyConfig config, 
            Transform target, Action<EnemyView> destroy)
        {
            _agent = enemyView.Agent;
            _hurtBox = enemyView.HurtBox;
            _enemyView = enemyView;
            _target = target;
            _destroy = destroy;
            _config = config;
            _moveService = ServiceLocator.Current.Get<MoveService>();
            _eventBus = ServiceLocator.Current.Get<EventBus>();
            AIPathfindingFrequency = ServiceLocator.Current.Get<ConfigProviderService>()
                .GetGameplayConfig().AIPathFindingFrequency;

            _agent.speed = _config.Speed;
            enemyView.HitBox.Damage = _config.Damage;
            _hurtBox.MaxHealth = _config.MaxHealth;
            _hurtBox.HealFullHealth();
            _hurtBox.Died += HurtBoxOnDied;
        }

        void HurtBoxOnDied()
        {
            _eventBus.TriggerEvent(new GameEvent(EventName.EnemyKilled,
                $"Enemy {_agent.gameObject.name} killed"));
            _destroy?.Invoke(_enemyView);
        }
        
        public void UpdatePath()
        {
            _moveService.MoveToTarget(_agent, _target.position);
        }
    }
}