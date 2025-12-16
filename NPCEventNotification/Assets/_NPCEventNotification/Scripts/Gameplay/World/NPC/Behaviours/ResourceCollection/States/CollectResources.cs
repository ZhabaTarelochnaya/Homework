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
        CancellationTokenSource _cts;

        public CollectResources(float collectionTime) : base(ResourceCollectionStateName.Collect)
        {
            _collectionTime = collectionTime;
        }
        public override void OnEnter()
        {
            _cts = new CancellationTokenSource();
            if (_nextState == ResourceCollectionStateName.GoToResourceZone) return;
            _nextState = ResourceCollectionStateName.Collect;
            Collect(_cts.Token);
        }

        public override void OnExit()
        {
            _cts?.Cancel();
        }
        public override ResourceCollectionStateName GetNextState()
        {
            if (_nextState == ResourceCollectionStateName.GoToResourceZone)
            {
                _nextState = ResourceCollectionStateName.Default;
                return ResourceCollectionStateName.GoToResourceZone;
            }
            return _nextState;
        }
        async void Collect(CancellationToken token)
        {
            try
            {
                var time = Time.time + _collectionTime;
                while (Time.time < time)
                {
                    token.ThrowIfCancellationRequested();
                    await Task.Yield();
                }
                _nextState = ResourceCollectionStateName.GoToStorage;
            }
            catch (OperationCanceledException)
            {
                _nextState = ResourceCollectionStateName.GoToResourceZone;
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
    }
}