using NPCEventNotification.Scripts.Utils.FiniteStateMachine;
using UnityEngine;
using UnityEngine.AI;

namespace NPCEventNotification.Scripts.Gameplay.NPC.Behaviours.ResourceCollection
{
    public class ResourceCollectionBehaviour : Behaviour
    {
        FSM<ResourceCollectionStateName> _fsm = new();
        public ResourceCollectionBehaviour(ResourceZone _resourceZone, NavMeshAgent _agent, Transform _storage)
            : base(BehaviourName.ResourceCollection)
        {
            _fsm.AddState(new GoToResourceZone(_resourceZone, _agent))
                .AddState(new CollectResources(2f))
                .AddState(new GoToStorage(_agent, _storage.position));
        }
        public override void Tick(float deltaTime)
        {
            _fsm.Tick(deltaTime);
        }

        public override void OnEnter()
        {
            _fsm.IsActive = true;
        }

        public override void OnExit()
        {
            _fsm.IsActive = false;
        }
    }
}