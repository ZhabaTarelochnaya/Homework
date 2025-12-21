using System.Collections.Generic;
using System.Linq;
using NPCEventNotification.Scripts.Gameplay.NPC;
using NPCEventNotification.Scripts.Gameplay.NPC.Behaviours;
using NPCEventNotification.Scripts.Gameplay.NPC.Behaviours.Types.Attack;
using NPCEventNotification.Scripts.Gameplay.NPC.Behaviours.Types.Chase;
using NPCEventNotification.Scripts.Gameplay.NPC.Behaviours.Wander;
using NPCEventNotification.Scripts.Utils;
using UnityEngine;
using UnityEngine.AI;

namespace NPCEventNotification.Scripts.Gameplay.World.NPC.Types.Warden
{
    public class GuardController : MonoBehaviour, IHealthUser
    {
        BehaviourManager _behaviourManager = new ();
        NavMeshAgent _agent;
        Transform _target;
        [SerializeField] GuardDataSO guardDataSo;
        [SerializeField] Detector2D _enemyDetector;
        [SerializeField] AttackZone _attackZone;
        GuardData _guardData = new ();

        public Health Health { get; private set; }

        void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();

            if (!_agent) Debug.LogError($"{gameObject.name}: _agent is not set");
            if (!_enemyDetector) Debug.LogError($"{gameObject.name}: _enemyDetector is not set");
            if (!guardDataSo) Debug.LogError($"{gameObject.name}: _wardenDataSO is not set");
            if (!_attackZone) Debug.LogError($"{gameObject.name}: _attackZone is not set");

            guardDataSo.FillData(_guardData);
            _guardData.CurrentHealth = _guardData.MaxHealth;
            Health = new Health(_guardData);
            _agent.speed = _guardData.Speed;

            _behaviourManager.Add(new WanderBehaviour(_agent, _guardData, this))
                .Add(new ChaseBehaviour(_agent, _guardData))
                .Add(new AttackBehaviour(_attackZone, _guardData));


            _enemyDetector.TriggerEntered += NpcDetectorOnTriggerEntered;
            _attackZone.TargetEntered += AttackZoneOnTargetEntered;
            _attackZone.TargetExited += AttackZoneOnTargetExited;
            Health.Died += () => Destroy(gameObject);
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
                _guardData.Destination = _target.position;
            }
            _behaviourManager.Tick(Time.fixedDeltaTime);
        }

        void OnDestroy()
        {
            _enemyDetector.TriggerEntered -= NpcDetectorOnTriggerEntered;
            _attackZone.TargetEntered -= AttackZoneOnTargetEntered;
            _attackZone.TargetExited -= AttackZoneOnTargetExited;
        }
    }
}