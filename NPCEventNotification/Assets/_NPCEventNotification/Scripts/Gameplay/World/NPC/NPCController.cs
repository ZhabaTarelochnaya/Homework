using NPCEventNotification.Scripts.Gameplay.NPC.Behaviours;
using NPCEventNotification.Scripts.Gameplay.NPC.Behaviours.ResourceCollection;
using UnityEngine;
using UnityEngine.AI;

namespace NPCEventNotification.Scripts.Gameplay.NPC
{
    public class NPCController : MonoBehaviour
    {
        BehaviourManager _behaviourManager = new ();
        NavMeshAgent _agent;
        [SerializeField] ResourceZone _resourceZone;
        [SerializeField] Transform _storage;
        public void Bind(EventManager manager)
        {
            
        }
        void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            
            if (!_agent) Debug.LogError($"{gameObject.name}: _agent is not set");
            if (!_resourceZone) Debug.LogError($"{gameObject.name}: _resourceZone is not set");
            if (!_storage) Debug.LogError($"{gameObject.name}: _storage is not set");
            
            _behaviourManager.Add(new ResourceCollectionBehaviour(_resourceZone, _agent, _storage));
        }

        void FixedUpdate()
        {
            _behaviourManager.Tick(Time.fixedDeltaTime);
        }
    }
}