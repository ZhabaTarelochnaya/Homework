using ConsoleTBS.Effects;

namespace ConsoleTBS.Potions;

public interface IConsumer
{
    public EffectProcessor EffectProcessor { get; }
    public IConsumable? CurrentConsumable { get; }
    public IEnumerable<IConsumable> Consumables { get; }
    public void Consume(IConsumable consumable);
    public void AddConsumable(IConsumable consumable);
    public IConsumable SwitchCurrentConsumable(int index);
    public bool TryConsumeCurrent(out IConsumable? consumable);
}