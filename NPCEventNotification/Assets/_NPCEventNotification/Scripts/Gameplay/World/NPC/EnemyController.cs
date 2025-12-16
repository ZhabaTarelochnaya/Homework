using System;
using NPCEventNotification.Scripts.Gameplay.NPC.Behaviours;
using NPCEventNotification.Scripts.Gameplay.NPC.Behaviours.Wander;
using UnityEngine;
using UnityEngine.AI;

namespace NPCEventNotification.Scripts.Gameplay.NPC
{
    public class EnemyController : MonoBehaviour
    {
        BehaviourManager _behaviourManager = new ();
        NavMeshAgent _agent;
        
        void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            
            if (!_agent) Debug.LogError($"{gameObject.name}: _agent is not set");
            
            _behaviourManager.Add(new WanderBehaviour(_agent, 10, 1));
        }

        void FixedUpdate()
        {
            _behaviourManager.Tick(Time.fixedDeltaTime);
        }
    }
}