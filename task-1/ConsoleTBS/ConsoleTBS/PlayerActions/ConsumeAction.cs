using ConsoleTBS.Potions;

namespace ConsoleTBS.PlayerActions;

public class ConsumeAction
{
    readonly Player _player;
    readonly ConsoleRenderer _renderer;
    
    public ConsumeAction(Player player, ConsoleRenderer renderer)
    {
        _player = player;
        _renderer = renderer;
    }
    public bool TryDo()
    {
        if (_player.TryConsumeCurrent(out IConsumable? consumable))
        {
            _player.EffectProcessor.ApplyEffects();
            _renderer.ConsumablesRenderer.ShowConsumedMessage(consumable);
            Console.ReadLine();
            return true;
        }
        _renderer.ConsumablesRenderer.ShowConsumableNotEquipped();
        Console.ReadLine();
        return false;
    }
}