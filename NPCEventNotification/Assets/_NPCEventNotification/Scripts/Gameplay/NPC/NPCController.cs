using System;
using NPCEventNotification.Scripts.Gameplay.NPC.Behaviours.ResourceCollection;
using NPCEventNotification.Scripts.Utils.FiniteStateMachine;
using UnityEngine;
using UnityEngine.AI;

namespace NPCEventNotification.Scripts.Gameplay.NPC
{
    public class NPCController : MonoBehaviour
    {
        FSM<ResourceCollectionStateName> _fsm = new();
        NavMeshAgent _agent;
        [SerializeField] ResourceZone _resourceZone;
        [SerializeField] Transform _storage;
        public void Bind()
        {
            
        }
        void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            if (!_agent) Debug.LogError($"{gameObject.name}: _agent is not set");
            var storagePos = new Vector2(_storage.position.x, _storage.position.y);
            _fsm.AddState(new GoToResourceZone(_resourceZone, _agent))
                .AddState(new CollectResources(2f))
                .AddState(new GoToStorage(_agent, storagePos));
        }

        void FixedUpdate()
        {
            _fsm.Tick(Time.fixedDeltaTime);
            Debug.Log(_fsm.CurrentState.StateName.ToString());
        }
    }
}