using System;
using System.Collections.Generic;
using System.Linq;
using NPCEventNotification.Scripts.Gameplay.NPC.Behaviours;
using NPCEventNotification.Scripts.Gameplay.NPC.Behaviours.Types.Attack;
using NPCEventNotification.Scripts.Gameplay.NPC.Behaviours.Types.Chase;
using NPCEventNotification.Scripts.Gameplay.NPC.Behaviours.Wander;
using NPCEventNotification.Scripts.Utils;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

namespace NPCEventNotification.Scripts.Gameplay.NPC
{
    public class EnemyController : MonoBehaviour, IHealthUser
    {
        BehaviourManager _behaviourManager = new ();
        NavMeshAgent _agent;
        Transform _target;
        [SerializeField] Detector2D _npcDetector;
        [SerializeField] EnemyDataSO _enemyDataSo;
        [SerializeField] AttackZone _attackZone;
        EnemyData _enemyData = new ();

        public Health Health { get; private set; }

        void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();

            if (!_agent) Debug.LogError($"{gameObject.name}: _agent is not set");
            if (!_npcDetector) Debug.LogError($"{gameObject.name}: _npcDetector is not set");
            if (!_enemyDataSo) Debug.LogError($"{gameObject.name}: _enemyDataSo is not set");
            if (!_attackZone) Debug.LogError($"{gameObject.name}: _attackZone is not set");

            _enemyDataSo.FillData(_enemyData);
            _enemyData.CurrentHealth = _enemyData.MaxHealth;
            Health = new Health(_enemyData);
            _agent.speed = _enemyData.Speed;

            _behaviourManager.Add(new WanderBehaviour(_agent, _enemyData, this))
                .Add(new ChaseBehaviour(_agent, _enemyData))
                .Add(new AttackBehaviour(_attackZone, _enemyData));


            _npcDetector.TriggerEntered += NpcDetectorOnTriggerEntered;
            _attackZone.TargetEntered += AttackZoneOnTargetEntered;
            _attackZone.TargetExited += AttackZoneOnTargetExited;
            Health.Died += () => DestroyImmediate(gameObject);
        }

        void AttackZoneOnTargetEntered(Health arg1, IEnumerable<Health> arg2)
        {
            _behaviourManager.SwitchBehaviour(BehaviourName.Attack);
        }

        void AttackZoneOnTargetExited(Health arg1, IEnumerable<Health> arg2)
        {
            if (arg2.Any()) return;
            if (arg1.CurrentHealth > 0)
            {
                _behaviourManager.SwitchBehaviour(BehaviourName.Chase);
                return;
            }
            _behaviourManager.SwitchBehaviour(BehaviourName.Wander);
        }

        void NpcDetectorOnTriggerEntered(Collider2D obj)
        {
            if (_target) return;
            _target = obj.transform;
            _behaviourManager.SwitchBehaviour(BehaviourName.Chase);
        }

        void FixedUpdate()
        {
            if (_target)
            {
                _enemyData.Destination = _target.position;
            }
            _behaviourManager.Tick(Time.fixedDeltaTime);
            Debug.Log(_behaviourManager.CurrentBehaviour.Name);
        }

        void OnDestroy()
        {
            _npcDetector.TriggerEntered -= NpcDetectorOnTriggerEntered;
            _attackZone.TargetEntered -= AttackZoneOnTargetEntered;
            _attackZone.TargetExited -= AttackZoneOnTargetExited;
        }
    }
}