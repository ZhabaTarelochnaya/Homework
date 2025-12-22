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
        EventManager _eventManager;
        NavMeshAgent _agent;
        List<Transform> _targets = new ();
        [SerializeField] GuardDataSO guardDataSo;
        [SerializeField] Detector2D _enemyDetector;
        [SerializeField] AttackZone _attackZone;
        GuardData _guardData = new ();
        
        public Health Health { get; private set; }

        public void Bind(EventManager manager)
        {
            _eventManager = manager;

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
            _eventManager.OnGameEvent += EventManagerOnGameEvent;
            Health.Died += () => DestroyImmediate(gameObject);
        }

        void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();

            if (!_agent) Debug.LogError($"{gameObject.name}: _agent is not set");
            if (!_enemyDetector) Debug.LogError($"{gameObject.name}: _enemyDetector is not set");
            if (!guardDataSo) Debug.LogError($"{gameObject.name}: _wardenDataSO is not set");
            if (!_attackZone) Debug.LogError($"{gameObject.name}: _attackZone is not set");
        }
        void FixedUpdate()
        {
            if (_targets.Count > 0 && _targets[0])
            {
                _guardData.Destination = _targets[0].position;
            }
            _behaviourManager.Tick(Time.fixedDeltaTime);
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
            
            _eventManager.TriggerEvent(new GameEvent(GameEventName.EnemyKilled, 
                $"Guard {gameObject.name} killed an enemy"));

            if (_targets.Count > 1) return;
            _eventManager.TriggerEvent(new GameEvent(GameEventName.AllEnemiesKilled,
                $"Guard {gameObject.name} killed last enemy"));
        }

        void NpcDetectorOnTriggerEntered(Collider2D obj)
        {
            _eventManager.TriggerEvent(new GameEvent(GameEventName.EnemyDetected, 
                $"Guard {gameObject.name} detected an enemy!",
                obj.transform));
        }
        void EventManagerOnGameEvent(GameEvent e)
        {
            Transform target;
            switch (e.Name)
            {
                case GameEventName.EnemyDetected:
                    target = e.Args[0] as Transform;
                    if (!target) Debug.LogError("Enemy transform wasn't passed to game event EnemyRoaming call");
                    if (!_targets.Contains(target))
                    {
                        _targets.Add(target);
                        _behaviourManager.SwitchBehaviour(BehaviourName.Chase);
                    }
                    break;
                case GameEventName.EnemyKilled:
                    _targets.RemoveAll(t => !t);
                    _behaviourManager.SwitchBehaviour(BehaviourName.Chase);
                    break;
                case GameEventName.AllEnemiesKilled:
                    _targets.Clear();
                    _behaviourManager.SwitchBehaviour(BehaviourName.Wander);
                    break;
            }
        }

        void OnDestroy()
        {
            _enemyDetector.TriggerEntered -= NpcDetectorOnTriggerEntered;
            _attackZone.TargetEntered -= AttackZoneOnTargetEntered;
            _attackZone.TargetExited -= AttackZoneOnTargetExited;
        }
    }
}