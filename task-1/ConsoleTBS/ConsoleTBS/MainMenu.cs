using ConsoleTBS.PlayerActions;

namespace ConsoleTBS;

public class MainMenu
{
    readonly AttackAction _attackAction;
    readonly ConsumeAction _consumeAction;
    readonly OpenShopAction _openShopAction;
    readonly SwitchConsumableAction _switchConsumableAction;
    readonly SwitchWeaponAction _switchWeaponAction;
    readonly ConsoleRenderer _renderer;

    public MainMenu(AttackAction attackAction, ConsumeAction consumeAction, OpenShopAction openShopAction,
        SwitchConsumableAction switchConsumableAction, SwitchWeaponAction switchWeaponAction, 
        ConsoleRenderer renderer)
    {
        _attackAction = attackAction;
        _consumeAction = consumeAction;
        _openShopAction = openShopAction;
        _switchConsumableAction = switchConsumableAction;
        _switchWeaponAction = switchWeaponAction;
        _renderer = renderer;
    }
    public bool Show(Enemy enemy)
    {
        while (true)
        {
            _renderer.ShowOptions();
            if (int.TryParse(Console.ReadLine(), out var option))
            {
                _renderer.Clear();
                switch (option)
                {
                    case 1:
                        _attackAction.Do(enemy);
                        return false;
                    case 2:
                        if (_consumeAction.TryDo()) return false;
                        break;
                    case 3:
                        _switchWeaponAction.Do();
                        break;
                    case 4:
                        _switchConsumableAction.Do();
                        break;
                    case 5:
                        _renderer.ShowPlayerStatus();
                        Console.ReadLine();
                        break;
                    case 6:
                        _openShopAction.Do();
                        break;
                    case 0:
                        return true;
                    default:
                        _renderer.Clear();
                        _renderer.ShowInvalidInput();
                        Console.ReadLine();
                        break;
                }
            }
            else
            {
                _renderer.Clear();
                _renderer.ShowInvalidInput();
                Console.ReadLine();
            }
            _renderer.Clear();
        }
    }
}