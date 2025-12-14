using NPCEventNotification.Scripts.Utils.FiniteStateMachine;
using UnityEngine;
using UnityEngine.AI;

namespace NPCEventNotification.Scripts.Gameplay.NPC.Behaviours.ResourceCollection
{
    public class GoToResourceZone : FSMState<ResourceCollectionStateName>
    {
        readonly ResourceZone _resourceZone;
        readonly NavMeshAgent _agent;

        public GoToResourceZone(ResourceZone resourceZone, NavMeshAgent agent) 
            : base(ResourceCollectionStateName.GoToResourceZone)
        {
            _resourceZone = resourceZone;
            _agent = agent;
        }

        public override void OnEnter()
        {
            var position = _resourceZone.GetRandomPosition();
            _agent.SetDestination(position);
        }

        public override ResourceCollectionStateName GetNextState()
        {
            if (_agent.remainingDistance <= _agent.stoppingDistance && !_agent.pathPending)
            {
                return ResourceCollectionStateName.Collect;
            }
            return StateName;
        }
    }
}