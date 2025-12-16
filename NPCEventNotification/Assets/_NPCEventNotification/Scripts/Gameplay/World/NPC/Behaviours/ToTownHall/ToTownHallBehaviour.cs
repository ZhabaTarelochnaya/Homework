using System.Threading.Tasks;
using NPCEventNotification.Scripts.Gameplay.NPC.Behaviours.ResourceCollection;
using UnityEngine.AI;

namespace NPCEventNotification.Scripts.Gameplay.NPC.Behaviours.ToTownHall
{
    public class ToTownHallBehaviour : Behaviour
    {
        readonly NavMeshAgent _agent;
        readonly TownHall _townHall;

        public ToTownHallBehaviour(NavMeshAgent agent, TownHall townHall) : base(BehaviourName.ToTownHall)
        {
            _agent = agent;
            _townHall = townHall;
        }

        public override void OnEnter()
        {
            _agent.SetDestination(_townHall.transform.position);
            HideWhenArrived();
        }

        async void HideWhenArrived()
        {
            while (_agent.remainingDistance > _agent.stoppingDistance || _agent.pathPending)
            {
                await Task.Yield();
            }
            _townHall.Hide();
        }
    }
}