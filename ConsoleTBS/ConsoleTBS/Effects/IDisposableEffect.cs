namespace ConsoleTBS.Effects;

public interface IDisposableEffect : IEffect
{
    void Dispose(ICharacter target);
}