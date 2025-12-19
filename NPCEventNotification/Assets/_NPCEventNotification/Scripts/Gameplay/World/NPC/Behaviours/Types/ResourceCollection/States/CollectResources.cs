using System;
using System.Threading;
using System.Threading.Tasks;
using NPCEventNotification.Scripts.Utils.FiniteStateMachine;
using UnityEngine;

namespace NPCEventNotification.Scripts.Gameplay.NPC.Behaviours.ResourceCollection
{
    public class CollectResources : FSMState<ResourceCollectionStateName>
    {
        readonly float _collectionTime;
        ResourceCollectionStateName _nextState;
        float _timer;
        public CollectResources(float collectionTime) : base(ResourceCollectionStateName.Collect)
        {
            _collectionTime = collectionTime;
        }
        public override void OnEnter()
        {
            if (_nextState == ResourceCollectionStateName.GoToResourceZone) return;
            _nextState = ResourceCollectionStateName.Collect;
            _timer = _collectionTime;
        }

        public override void Tick(float deltaTime)
        {
            _timer -= deltaTime;
        }

        public override void OnExit()
        {
            if (_timer > 0)
            {
                _nextState = ResourceCollectionStateName.GoToResourceZone;
                _timer = 0;
            }
        }
        public override ResourceCollectionStateName GetNextState()
        {
            if (_nextState == ResourceCollectionStateName.GoToResourceZone)
            {
                _nextState = ResourceCollectionStateName.Default;
                return ResourceCollectionStateName.GoToResourceZone;
            }
            if (_timer <= 0) return ResourceCollectionStateName.GoToStorage; 
            return _nextState;
        }
    }
}