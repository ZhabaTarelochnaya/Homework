namespace ConsoleTBS.PlayerActions;

public class SwitchConsumableAction
{
    readonly Player _player;
    readonly ConsoleRenderer _renderer;

    public SwitchConsumableAction(Player player, ConsoleRenderer renderer)
    {
        _player = player;
        _renderer = renderer;
    }
    public void Do()
    {
        _renderer.Clear();
        if (!_player.Consumables.Any())
        {
            _renderer.ShowEmpty();
            Console.ReadLine();
            return;
        }
        
        _renderer.ConsumablesRenderer.ShowAvailableConsumables(_player.Consumables);
        Console.WriteLine("0 - Exit");
        if (int.TryParse(Console.ReadLine(), out var index))
        {
            if (index == 0) return;
            _renderer.Clear();
            index--;
            if (index >= _player.Consumables.Count())
            {
                _renderer.ShowInvalidInput();
                Console.ReadLine();
                return;
            }
            _player.SwitchCurrentConsumable(index);
            _renderer.ConsumablesRenderer.ShowCurrentConsumable(_player);
        }
        else
        {
            _renderer.Clear();
            _renderer.ShowInvalidInput();
        }
        Console.ReadLine();
        _renderer.Clear();
    }
}