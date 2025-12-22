namespace NPCEventNotification.Scripts.Gameplay
{
    public class GameEvent
    {
        public GameEventName Name  { get; }
        public float Time { get; }
        public string Description { get; }
        public object[] Args { get; }

        public GameEvent(GameEventName name, string description)
        {
            Name = name;
            Time = UnityEngine.Time.time;
            Description = description;
        }
        public GameEvent(GameEventName name, string description, params object[] args)
        {
            Name = name;
            Time = UnityEngine.Time.time;
            Description = description;
            Args = args;
        }
    }
}