using System;
using NPCEventNotification.Scripts.Utils.FiniteStateMachine;
using UnityEngine;
using UnityEngine.AI;

namespace NPCEventNotification.Scripts.Gameplay.NPC.Behaviours.ResourceCollection
{
    public class ResourceCollectionBehaviour : Behaviour
    {
        readonly NavMeshAgent _agent;
        FSM<ResourceCollectionStateName> _fsm = new();
        readonly int _area = 1 << NavMesh.GetAreaFromName("Worker");
        public ResourceCollectionBehaviour(ResourceZone resourceZone, NavMeshAgent agent, Transform storage)
            : base(BehaviourName.ResourceCollection)
        {
            _agent = agent;
            _fsm.AddState(new GoToResourceZone(resourceZone, agent))
                .AddState(new CollectResources(resourceZone.CollectionTime))
                .AddState(new GoToStorage(agent, storage.position));
        }
        public override void Tick(float deltaTime)
        {
            _fsm.Tick(deltaTime);
        }

        public override void OnEnter()
        {
            _fsm.IsActive = true;
            _agent.areaMask |= _area;
        }

        public override void OnExit()
        {
            _fsm.IsActive = false;
            _agent.areaMask &= ~_area;
        }
    }
}