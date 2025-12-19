using NPCEventNotification.Scripts.Utils;

namespace NPCEventNotification.Scripts.Gameplay.NPC.Behaviours.Types.Attack
{
    public class AttackBehaviour : Behaviour
    {
        float _timer;
        readonly AttackZone _attackZone;
        readonly IAttackerData _attackerData;

        public AttackBehaviour(AttackZone attackZone, IAttackerData attackerData) 
            : base(BehaviourName.Attack)
        {
            _attackZone = attackZone;
            _attackerData = attackerData;
        }

        public override void Tick(float deltaTime)
        {
            _timer -= deltaTime;
            if (_timer > 0) return;
            if (_attackZone.TryAttack(_attackerData.Damage))
            {
                _timer = _attackerData.AttackCooldown;
            }
        }
    }
}