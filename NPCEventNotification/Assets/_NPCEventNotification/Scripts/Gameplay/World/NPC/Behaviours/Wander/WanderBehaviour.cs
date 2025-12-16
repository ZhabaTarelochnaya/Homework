using System;
using System.Threading;
using System.Threading.Tasks;
using NPCEventNotification.Scripts.Utils.Extansions;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace NPCEventNotification.Scripts.Gameplay.NPC.Behaviours.Wander
{
    public class WanderBehaviour : Behaviour
    {
        readonly NavMeshAgent _agent;
        readonly float _wanderDistance;
        readonly float _waitTime;
        CancellationTokenSource _cts;

        public WanderBehaviour(NavMeshAgent agent, float wanderDistance, float waitTime) 
            : base(BehaviourName.Wander)
        {
            _agent = agent;
            _wanderDistance = wanderDistance;
            _waitTime = waitTime;
        }

        public override void OnEnter()
        {
            _cts = new CancellationTokenSource();
            Wander(_cts);
        }

        public override void OnExit()
        {
            _cts.Cancel();
        }

        async void Wander(CancellationTokenSource cts)
        {
            try
            {
                while (!cts.IsCancellationRequested)
                {
                    Vector3 randomDirection = Random.insideUnitSphere * _wanderDistance;
                    randomDirection += _agent.transform.position;
                    NavMeshHit hit;
                    NavMesh.SamplePosition(randomDirection, out hit, _wanderDistance, _agent.areaMask);
                    _agent.SetDestination(hit.position);
                    while (_agent.remainingDistance > _agent.stoppingDistance || _agent.pathPending)
                    {
                        await Task.Yield();
                    }
                    await UnityTask.WaitForSeconds(_waitTime);
                }
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
    }
}