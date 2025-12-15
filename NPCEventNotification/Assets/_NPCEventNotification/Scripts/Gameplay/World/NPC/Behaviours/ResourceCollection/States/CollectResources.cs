using System;
using System.Threading.Tasks;
using NPCEventNotification.Scripts.Utils.FiniteStateMachine;
using UnityEngine;

namespace NPCEventNotification.Scripts.Gameplay.NPC.Behaviours.ResourceCollection
{
    public class CollectResources : FSMState<ResourceCollectionStateName>
    {
        readonly float _collectionTime;
        bool _isCollected;

        public CollectResources(float collectionTime) : base(ResourceCollectionStateName.Collect)
        {
            _collectionTime = collectionTime;
        }
        public override void OnEnter()
        {
            Collect();
        }
        public override ResourceCollectionStateName GetNextState()
        {
            if (_isCollected)
            {
                _isCollected = false;
                return ResourceCollectionStateName.GoToStorage;
            }
            return ResourceCollectionStateName.Collect;
        }
        async void Collect()
        {
            var time = Time.time + _collectionTime;
            while (Time.time < time)
            {
                await Task.Yield();
            }
            _isCollected = true;
        }
    }
}