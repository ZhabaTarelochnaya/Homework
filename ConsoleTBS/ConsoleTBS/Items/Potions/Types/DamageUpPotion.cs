using ConsoleTBS.Effects;

namespace ConsoleTBS.Potions;

public class DamageUpPotion : Item, IConsumable
{
    readonly int _duration;
    readonly int _damageUp;
    readonly DamageUp _damageUpEffect;
    public override ItemName Name => ItemName.DamageUpPotion;
    
    public DamageUpPotion(int damageUp, int duration, int price) : base(price)
    {
        _damageUpEffect = new DamageUp(damageUp, duration);
        _damageUp = damageUp;
        _duration = duration;
    }
    public IEffect Consume() => _damageUpEffect;
    public override void Use(ICharacter character) => character.AddConsumable(this);
    public string GetDescription() => $"Increases damage by {_damageUp}. Duration: {_duration} rounds.";
}