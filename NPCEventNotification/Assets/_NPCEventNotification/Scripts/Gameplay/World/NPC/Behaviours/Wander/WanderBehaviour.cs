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

        public WanderBehaviour(NavMeshAgent agent, float wanderDistance, float waitTime, MonoBehaviour actor) 
            : base(BehaviourName.Wander)
        {
            _agent = agent;
            _wanderDistance = wanderDistance;
            _waitTime = waitTime;
            _actor = actor;
        }

        public override void OnEnter()
        {
            _actor.StartCoroutine(Wander());
        }

        public override void OnExit()
        {
            _actor.StopCoroutine(Wander());
            _agent.SetDestination(_agent.transform.position);
        }
        IEnumerator Wander()
        {
            while (true)
            {
                Vector3 randomDirection = Random.insideUnitSphere * _wanderDistance;
                randomDirection += _agent.transform.position;
                NavMeshHit hit;
                NavMesh.SamplePosition(randomDirection, out hit, _wanderDistance, _agent.areaMask);
                _agent.SetDestination(hit.position);
                yield return new WaitWhile(IsMoving);
                yield return new WaitForSeconds(_waitTime);
            }
        }

        bool IsMoving() => _agent.remainingDistance > _agent.stoppingDistance || _agent.pathPending;
    }
}