using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace NPCEventNotification.Scripts.Gameplay.NPC.Behaviours.Types.Chase
{
    public class ChaseBehaviour : Behaviour
    {
        readonly NavMeshAgent _agent;
        readonly IAgentData _agentData;
        Coroutine _coroutine;
        float _timer;
        public ChaseBehaviour(NavMeshAgent agent, IAgentData agentData) 
            : base(BehaviourName.Chase)
        {
            _agent = agent;
            _agentData = agentData;
        }

        public override void OnEnter()
        {
            _agent.SetDestination(_agentData.Destination);
        }
        public override void Tick(float deltaTime)
        {
            _timer -= deltaTime;
            if (_timer <= 0)
            {
                _agent.SetDestination(_agentData.Destination);
                _timer = 1 / _agentData.SetDestinationFrequency;
            }
        }
        public override void OnExit()
        {
            _agent.SetDestination(_agent.transform.position);
        }
    }
}