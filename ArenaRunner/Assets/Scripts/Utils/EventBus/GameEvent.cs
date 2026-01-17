using System;

namespace Utils.EventBus
{
    public class GameEvent
    {
        public EventName Name { get; }
        public string Description { get; }
        public float Time => UnityEngine.Time.time;
        public object[] Args { get; }
        
        public GameEvent(EventName name, string description)
        {
            Name = name;
            Description = description;
        }
        public GameEvent(EventName name, string description, params object[] args)
        {
            Name = name;
            Description = description;
            Args = args;
        }

        public override string ToString()
        {
            return $"({Time:F2}) {Name}: {Description}";
        }
    }
}