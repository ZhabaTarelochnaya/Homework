using NPCEventNotification.Scripts.Gameplay.NPC.Behaviours;
using NPCEventNotification.Scripts.Gameplay.NPC.Behaviours.ResourceCollection;
using NPCEventNotification.Scripts.Gameplay.NPC.Behaviours.ToTownHall;
using NPCEventNotification.Scripts.Utils;
using UnityEngine;
using UnityEngine.AI;

namespace NPCEventNotification.Scripts.Gameplay.NPC
{
    public class WorkerController : MonoBehaviour, IHealthUser
    {
        EventManager _eventManager;
        BehaviourManager _behaviourManager = new ();
        NavMeshAgent _agent;
        [SerializeField] WorkerDataSO _workerDataSO;
        [SerializeField] Detector2D _enemyDetector;
        WorkerData _workerData;
        public Health Health { get; private set; }
        
        public void Bind(EventManager manager, ResourceZone resourceZone, TownHall townHall, Transform storage)
        {
            _eventManager = manager;
            _workerData = new WorkerData();
            _workerDataSO.Fill(_workerData);
            _workerData.CurrentHealth = _workerDataSO.MaxHealth;
            Health = new Health(_workerData);
            
            _behaviourManager.Add(new ResourceCollectionBehaviour(resourceZone, _agent, storage))
                .Add(new ToTownHallBehaviour(_agent, townHall, this));

            Health.Died += () => DestroyImmediate(gameObject);
            _eventManager.OnGameEvent += OnGameEvent;
            _enemyDetector.TriggerEntered += EnemyDetectorOnTriggerEntered;
        }
        void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            if (!_agent) Debug.LogError($"{gameObject.name}: _agent is not set");
            if (!_workerDataSO) Debug.LogError($"{gameObject.name}: _workerDataSO is not set");
            if (!_enemyDetector) Debug.LogError($"{gameObject.name}: _enemyDetector is not set");
        }

        void FixedUpdate()
        {
            _behaviourManager.Tick(Time.fixedDeltaTime);
        }

        void OnGameEvent(GameEvent e)
        {
            switch (e.Name)
            {
                case GameEventName.Morning:
                    if (_workerData.IsAvoidingEnemies) break;
                    _behaviourManager.SwitchBehaviour(BehaviourName.ResourceCollection);
                    break;
                case GameEventName.Evening:
                    _behaviourManager.SwitchBehaviour(BehaviourName.ToTownHall);
                    break;
                case GameEventName.EnemyDetected:
                    _behaviourManager.SwitchBehaviour(BehaviourName.ToTownHall);
                    _workerData.IsAvoidingEnemies = true;
                    break;
                case GameEventName.AllEnemiesKilled:
                    _behaviourManager.SwitchBehaviour(BehaviourName.ResourceCollection);
                    _workerData.IsAvoidingEnemies = false;
                    break;
            }
        }

        void EnemyDetectorOnTriggerEntered(Collider2D obj)
        {
            var e = new GameEvent(GameEventName.EnemyDetected, 
                $"Worker {gameObject.name} detected an enemy!",
                new[] { obj.transform });
            _eventManager.TriggerEvent(e);
        }


        void OnDestroy()
        {
            if (_eventManager == null) return;
            _eventManager.OnGameEvent -= OnGameEvent;
        }

    }
}