namespace NPCEventNotification.Scripts.Gameplay
{
    public class GameEvent
    {
        public GameEventName Name  { get; }
        public float Time { get; }
        public string Description { get; }

        public GameEvent(GameEventName name, float time, string description)
        {
            Name = name;
            Time = time;
            Description = description;
        }
    }
}