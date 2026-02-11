namespace _Prototype.Scripts.Utils.EventBus
{
    public class GameEvent
    {
        public EventName Name { get; }
        public string Description { get; }
        public float Time { get; }
        public object[] Args { get; }
        
        public GameEvent(EventName name, string description)
        {
            Name = name;
            Description = description;
            Time = UnityEngine.Time.time;
        }
        public GameEvent(EventName name, string description, params object[] args)
        {
            Name = name;
            Description = description;
            Args = args;
            Time = UnityEngine.Time.time;
        }

        public override string ToString()
        {
            return $"({Time:F2}) {Name}: {Description}";
        }
    }
}