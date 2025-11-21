using ConsoleTBS.Potions;

namespace ConsoleTBS.Effects;

public class DamageUp : IDisposableEffect
{
    readonly int _amount;
    readonly int _duration;
    public int TurnsLeft { get; private set; }
    public EffectName Name => EffectName.DamageUp;
    
    public DamageUp(int amount, int duration)
    {
        _amount = amount;
        _duration = duration;
        TurnsLeft = duration;
    }

    public void Apply(ICharacter character)
    {
        if (TurnsLeft == _duration) character.BaseDamage += _amount;
        TurnsLeft--;
    }

    public string GetDescription() => $"+{_amount} damage.";

    public void Dispose(ICharacter character) => character.BaseDamage -= _amount;
}