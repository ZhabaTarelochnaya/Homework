namespace ConsoleTBS.Effects;

public class EffectProcessor
{
    readonly ICharacter _effectTaget;
    List<IEffect> _currentEffects = new ();
    public IEnumerable<IEffect> CurrentEffects => _currentEffects;
    public EffectProcessor(ICharacter effectTaget)
    {
        _effectTaget = effectTaget;
    }
    public void Add(IEffect effect) => _currentEffects.Add(effect);

    public void ApplyEffects()
    {
        _currentEffects.ForEach(e => e.Apply(_effectTaget));
    }

    public void DisposeCompleted()
    {
        _currentEffects.Where(e => e.TurnsLeft <= 0)
            .ToList()
            .ForEach(e =>
            {
                if (e is IDisposableEffect disposableEffect)
                {
                    disposableEffect.Dispose(_effectTaget);
                }
                _currentEffects.Remove(e);
            });
    }
}