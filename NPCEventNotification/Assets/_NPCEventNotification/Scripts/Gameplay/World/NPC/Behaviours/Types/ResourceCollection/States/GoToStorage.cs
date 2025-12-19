using NPCEventNotification.Scripts.Utils.FiniteStateMachine;
using UnityEngine;
using UnityEngine.AI;

namespace NPCEventNotification.Scripts.Gameplay.NPC.Behaviours.ResourceCollection
{
    public class GoToStorage : FSMState<ResourceCollectionStateName>
    {
        readonly NavMeshAgent _agent;
        readonly Vector2 _storagePos;

        public GoToStorage(NavMeshAgent agent, Vector2 storagePos) 
            : base(ResourceCollectionStateName.GoToStorage)
        {
            _agent = agent;
            _storagePos = storagePos;
        }

        public override void OnEnter()
        {
            _agent.SetDestination(_storagePos);
        }
        
        public override ResourceCollectionStateName GetNextState()
        {
            if (_agent.remainingDistance <= _agent.stoppingDistance && !_agent.pathPending)
            {
                return ResourceCollectionStateName.GoToResourceZone;
            }
            return StateName;
        }
    }
}