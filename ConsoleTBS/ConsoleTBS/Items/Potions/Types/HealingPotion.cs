using ConsoleTBS.Effects;

namespace ConsoleTBS.Potions;

public class HealingPotion : Item, IConsumable
{
    readonly int _healAmount;
    readonly Heal _healEffect;
    
    public override ItemName Name => ItemName.HealingPotion;
    
    public HealingPotion(int healAmount, int price) : base(price)
    {
        _healAmount = healAmount;
        _healEffect = new Heal(healAmount, 1);
    }
    public override void Use(ICharacter character) => character.Consume(this);
    public IEffect Consume() => _healEffect;
    public string GetDescription() => $"Restores {_healAmount} health.";
}