namespace ConsoleTaskManager.Menu;

public class ViewTasksMenu
{
    readonly ConsoleRenderer _renderer;
    readonly GeneralMenu _generalMenu;

    public ViewTasksMenu(ConsoleRenderer renderer, GeneralMenu generalMenu)
    {
        _renderer = renderer;
        _generalMenu = generalMenu;
    }
    public void Show()
    {
        while (true)
        {
            _renderer.Clear();
            _renderer.ShowTasks();
            _renderer.ShowViewMenu();
            if (int.TryParse(Console.ReadLine(), out var index))
            {
                _renderer.Clear();
                switch (index)
                {
                    case 1:
                        var category = _generalMenu.ChooseCategory();
                        _renderer.Clear();
                        _renderer.ShowTasksByCategory(category);
                        break;
                    case 2:
                        var priority = _generalMenu.ChoosePriority();
                        _renderer.Clear();
                        _renderer.ShowTasksByPriority(priority);
                        break;
                    case 3:
                        var status = _generalMenu.ChooseStatus();
                        _renderer.Clear();
                        _renderer.ShowTasksByStatus(status);
                        break;
                    case 0:
                        _renderer.Clear();
                        return;
                    default:
                        _renderer.Clear();
                        _renderer.ShowInvalidOptionMessage();
                        break;
                }

                Console.ReadLine();
            }
            else
            {
                _renderer.Clear();
                _renderer.ShowInvalidOptionMessage();
                Console.ReadLine();
            }
        }
    }
}