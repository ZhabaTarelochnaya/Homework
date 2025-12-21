using System.Collections;
using NPCEventNotification.Scripts.Gameplay.NPC.Behaviours.ResourceCollection;
using UnityEngine;
using UnityEngine.AI;

namespace NPCEventNotification.Scripts.Gameplay.NPC.Behaviours.ToTownHall
{
    public class ToTownHallBehaviour : Behaviour
    {
        readonly NavMeshAgent _agent;
        readonly TownHall _townHall;
        readonly MonoBehaviour _actor;
        readonly int _area = 1 << NavMesh.GetAreaFromName("ToTownHall");
        Coroutine _coroutine;
        
        public ToTownHallBehaviour(NavMeshAgent agent, TownHall townHall, MonoBehaviour actor) 
            : base(BehaviourName.ToTownHall)
        {
            _agent = agent;
            _townHall = townHall;
            _actor = actor;
        }

        public override void OnEnter()
        {
            _agent.areaMask |= _area;
            _agent.SetDestination(_townHall.transform.position);
            _coroutine = _actor.StartCoroutine(HideWhenArrived());
        }

        public override void OnExit()
        {
            _agent.areaMask &= ~_area;
            _agent.SetDestination(_agent.transform.position);
            _actor.StopCoroutine(_coroutine);
        }

        IEnumerator HideWhenArrived()
        {
            yield return new WaitWhile(IsTargetReached);
            _townHall.Hide();
        }
        bool IsTargetReached() => _agent.remainingDistance > _agent.stoppingDistance || _agent.pathPending;
    }
}