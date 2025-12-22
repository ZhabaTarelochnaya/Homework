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
        [SerializeField] WorkerDataSO _workerData;
        [SerializeField] Detector2D _enemyDetector;

        public Health Health { get; private set; }
        
        public void Bind(EventManager manager, ResourceZone resourceZone, TownHall townHall, Transform storage)
        {
            _eventManager = manager;
            var workerData = new WorkerData();
            _workerData.Fill(workerData);
            workerData.CurrentHealth = _workerData.MaxHealth;
            Health = new Health(workerData);
            
            _behaviourManager.Add(new ResourceCollectionBehaviour(resourceZone, _agent, storage))
                .Add(new ToTownHallBehaviour(_agent, townHall, this));

            Health.Died += () => DestroyImmediate(gameObject);
            _eventManager.OnGameEvent += OnGameEvent;
        }

        void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            if (!_agent) Debug.LogError($"{gameObject.name}: _agent is not set");
            if (!_enemyDetector) Debug.LogError($"{gameObject.name}: _enemyDetector is not set");
        }

        void FixedUpdate()
        {
            _behaviourManager.Tick(Time.fixedDeltaTime);
        }

        void OnGameEvent(GameEvent e)
        {
            if (e.Name == GameEventName.Morning)
            {
                _behaviourManager.SwitchBehaviour(BehaviourName.ResourceCollection);
            } 
            else if (e.Name == GameEventName.Evening)
            {
                _behaviourManager.SwitchBehaviour(BehaviourName.ToTownHall);
            }
        }

        void OnDestroy()
        {
            if (_eventManager == null) return;
            _eventManager.OnGameEvent -= OnGameEvent;
        }

    }
}