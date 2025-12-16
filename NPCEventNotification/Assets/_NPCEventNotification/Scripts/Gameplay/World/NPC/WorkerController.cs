using NPCEventNotification.Scripts.Gameplay.NPC.Behaviours;
using NPCEventNotification.Scripts.Gameplay.NPC.Behaviours.ResourceCollection;
using NPCEventNotification.Scripts.Gameplay.NPC.Behaviours.ToTownHall;
using UnityEngine;
using UnityEngine.AI;

namespace NPCEventNotification.Scripts.Gameplay.NPC
{
    public class WorkerController : MonoBehaviour
    {
        BehaviourManager _behaviourManager = new ();
        NavMeshAgent _agent;
        [SerializeField] ResourceZone _resourceZone;
        [SerializeField] Transform _storage;
        [SerializeField] TownHall _townHall;
        public void Bind(EventManager manager)
        {
            manager.OnGameEvent += OnGameEvent;
        }

        void OnGameEvent(GameEvent e)
        {
            if (e.Name == GameEventName.Day)
            {
                _behaviourManager.SwitchBehaviour(BehaviourName.ResourceCollection);
            } 
            else if (e.Name == GameEventName.Night)
            {
                _behaviourManager.SwitchBehaviour(BehaviourName.ToTownHall);
            }
        }

        void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            
            if (!_agent) Debug.LogError($"{gameObject.name}: _agent is not set");
            if (!_resourceZone) Debug.LogError($"{gameObject.name}: _resourceZone is not set");
            if (!_storage) Debug.LogError($"{gameObject.name}: _storage is not set");
            
            _behaviourManager.Add(new ResourceCollectionBehaviour(_resourceZone, _agent, _storage))
                .Add(new ToTownHallBehaviour(_agent, _townHall));
        }

        void FixedUpdate()
        {
            _behaviourManager.Tick(Time.fixedDeltaTime);
        }
    }
}