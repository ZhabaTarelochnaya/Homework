namespace NPCEventNotification.Scripts.Gameplay.NPC.Behaviours
{
    public abstract class Behaviour
    {
        public BehaviourName Name { get; }
        public Behaviour(BehaviourName name) => Name = name;
        public virtual void OnEnter() {}
        public virtual void OnExit() {}
        public virtual void Tick(float deltaTime) {}
    }
}