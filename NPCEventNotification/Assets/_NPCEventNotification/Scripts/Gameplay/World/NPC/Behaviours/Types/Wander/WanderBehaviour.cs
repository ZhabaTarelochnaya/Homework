using System.Collections;
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
        readonly MonoBehaviour _actor;
        Coroutine _corutine;

        public WanderBehaviour(NavMeshAgent agent, IWandererData wandererData, MonoBehaviour actor) 
            : base(BehaviourName.Wander)
        {
            _agent = agent;
            _wanderDistance = wandererData.WanderDistance;
            _waitTime = wandererData.WanderStopTime;
            _actor = actor;
        }

        public override void OnEnter()
        {
            _corutine = _actor.StartCoroutine(Wander());
        }

        public override void OnExit()
        {
            _actor.StopCoroutine(_corutine);
            if ( !_agent || !_agent.isOnNavMesh) return;
            _agent.SetDestination(_agent.transform.position);
        }
        IEnumerator Wander()
        {
            while (true)
            {
                Vector3 randomDirection = Random.insideUnitCircle * _wanderDistance;
                randomDirection += _agent.transform.position;
                NavMeshHit hit;
                var pointFound = NavMesh.SamplePosition(randomDirection, out hit, _wanderDistance, _agent.areaMask);
                if (pointFound && _agent && _agent.isOnNavMesh)
                {
                    _agent.SetDestination(hit.position);
                }
                yield return new WaitWhile(IsMoving);
                yield return new WaitForSeconds(_waitTime);
            }
        }

        bool IsMoving() => _agent.remainingDistance > _agent.stoppingDistance || _agent.pathPending;
    }
}