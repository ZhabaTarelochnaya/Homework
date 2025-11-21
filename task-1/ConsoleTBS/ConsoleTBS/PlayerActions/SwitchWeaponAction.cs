namespace ConsoleTBS.PlayerActions;

public class SwitchWeaponAction
{
    readonly Player _player;
    readonly ConsoleRenderer _renderer;

    public SwitchWeaponAction(Player player, ConsoleRenderer renderer)
    {
        _player = player;
        _renderer = renderer;
    }
    public void Do()
    {
        _renderer.Clear();
        if (!_player.Weapons.Any())
        {
            _renderer.ShowEmpty();
            Console.ReadLine();
            return;
        }
        
        _renderer.WeaponsRenderer.ShowAvailableWeapons(_player.Weapons);
        Console.WriteLine("0 - Exit");
        if (int.TryParse(Console.ReadLine(), out var index))
        {
            if (index == 0) return;
            _renderer.Clear();
            index--;
            if (index >= _player.Weapons.Count())
            {
                _renderer.ShowInvalidInput();
                Console.ReadLine();
                return;
            }
            _player.SwitchCurrentWeapon(index);
            _renderer.WeaponsRenderer.ShowCurrentWeapon(_player);
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