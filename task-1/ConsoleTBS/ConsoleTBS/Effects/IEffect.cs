namespace ConsoleTBS.Effects;

public interface IEffect 
{
    public EffectName Name { get; }
    public int TurnsLeft { get; }
    void Apply(ICharacter target);
    string GetDescription();
}