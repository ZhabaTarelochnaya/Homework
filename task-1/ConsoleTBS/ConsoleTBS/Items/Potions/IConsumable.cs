using ConsoleTBS.Effects;

namespace ConsoleTBS.Potions;

public interface IConsumable
{
    ItemName Name { get; }
    IEffect Consume();
    string GetDescription();
}