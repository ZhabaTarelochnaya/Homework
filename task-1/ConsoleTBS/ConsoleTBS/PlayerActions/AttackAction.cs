namespace ConsoleTBS.PlayerActions;

public class AttackAction
{
    readonly Player _player;
    readonly ConsoleRenderer _renderer;

    public AttackAction(Player player, ConsoleRenderer renderer)
    {
        _player = player;
        _renderer = renderer;
    }
    public void Do(Enemy enemy)
    {
        _player.EffectProcessor.ApplyEffects();
        var damage = _player.Attack(enemy);
        _renderer.ShowAttackInfo(enemy, damage);
        Console.ReadLine();
    }
}