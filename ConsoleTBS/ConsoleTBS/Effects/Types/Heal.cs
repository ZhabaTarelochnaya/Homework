namespace ConsoleTBS.Effects;

public class Heal : IEffect
{
    readonly int _amountPerRound;

    public EffectName Name => EffectName.Heal;
    public int TurnsLeft { get; private set; }

    public Heal(int amountPerRound, int turnsLeft)
    {
        _amountPerRound = amountPerRound;
        TurnsLeft = turnsLeft;
    }
    public void Apply(ICharacter character)
    {
        character.Heal(_amountPerRound);
        TurnsLeft--;
    }

    public string GetDescription() => $"+{_amountPerRound} hp/round.";
}