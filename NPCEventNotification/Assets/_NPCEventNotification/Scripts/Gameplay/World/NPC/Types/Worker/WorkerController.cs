using NPCEventNotification.Scripts.Gameplay.NPC.Behaviours;
using NPCEventNotification.Scripts.Gameplay.NPC.Behaviours.ResourceCollection;
using NPCEventNotification.Scripts.Gameplay.NPC.Behaviours.ToTownHall;
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
        [SerializeField] ResourceZone _resourceZone;
        [SerializeField] Transform _storage;
        [SerializeField] TownHall _townHall;

        public Health Health { get; private set; }
        
        public void Bind(EventManager manager)
        {
            _eventManager = manager;
            var workerData = new WorkerData();
            _workerData.Fill(workerData);
            workerData.CurrentHealth = _workerData.MaxHealth;
            Health = new Health(workerData);
            
            _behaviourManager.Add(new ResourceCollectionBehaviour(_resourceZone, _agent, _storage))
                .Add(new ToTownHallBehaviour(_agent, _townHall, this));

            Health.Died += () => Destroy(gameObject);
            _eventManager.OnGameEvent += OnGameEvent;
        }

        void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            if (!_agent) Debug.LogError($"{gameObject.name}: _agent is not set");
            if (!_resourceZone) Debug.LogError($"{gameObject.name}: _resourceZone is not set");
            if (!_storage) Debug.LogError($"{gameObject.name}: _storage is not set");
        }

        void FixedUpdate()
        {
            if(Input.GetKeyDown(KeyCode.Space)) Destroy(this);
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