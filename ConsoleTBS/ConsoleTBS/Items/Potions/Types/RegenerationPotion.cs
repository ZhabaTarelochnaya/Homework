using ConsoleTBS.Potions;

namespace ConsoleTBS.Effects;

public class RegenerationPotion : Item, IConsumable
{
    readonly int _regen;
    readonly int _duration;
    readonly Heal _heal;
    public override ItemName Name => ItemName.RegenerationPotion;

    public RegenerationPotion(int regen, int duration, int price) : base(price)
    {
        _regen = regen;
        _duration = duration;
        _heal = new Heal(_regen, _duration);
    }
    public IEffect Consume() => _heal;
    public override void Use(ICharacter character) => character.Consume(this);
    public string GetDescription() => $"Restores {_regen} health each round. Duration: {_duration}.";
}