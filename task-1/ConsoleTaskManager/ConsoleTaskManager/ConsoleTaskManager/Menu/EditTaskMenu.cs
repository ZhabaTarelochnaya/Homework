namespace ConsoleTaskManager.Menu;

public class EditTaskMenu
{
    readonly ConsoleRenderer _renderer;
    readonly TaskManager _taskManager;
    readonly GeneralMenu _generalMenu;

    public EditTaskMenu(ConsoleRenderer renderer, TaskManager taskManager, GeneralMenu generalMenu)
    {
        _renderer = renderer;
        _taskManager = taskManager;
        _generalMenu = generalMenu;
    }
    public void Show()
    {
        _renderer.Clear();
        _renderer.ShowEnterTaskNumber();
        if (int.TryParse(Console.ReadLine(), out var index))
        {
            if (_taskManager.TryGetTask(--index, out var task))
            {
                task = TaskBuilder.FromTask(task).BuildData();
                ShowEditSubMenu(task, index);
            }
            else
            {
                _renderer.Clear();
                _renderer.ShowInvalidOptionMessage();
                Console.ReadLine();
            }
        }
        else
        {
            _renderer.Clear();
            _renderer.ShowInvalidOptionMessage();
            Console.ReadLine();
        }
    }

    void ShowEditSubMenu(TaskData task, int index)
    {
        while (true)
        {
            _renderer.Clear();
            var taskItem = TaskBuilder.FromTask(task).Build();
            _renderer.ShowEditTaskMenu(taskItem, index);
            if (int.TryParse(Console.ReadLine(), out var input))
            {
                _renderer.Clear();
                switch (input)
                {
                    case 1:
                        task.Name = _generalMenu.ChooseName();
                        break;
                    case 2:
                        task.Description = _generalMenu.ChooseDescription();
                        break;
                    case 3:
                        task.Category = _generalMenu.ChooseCategory();
                        break;
                    case 4:
                        task.Priority = _generalMenu.ChoosePriority();
                        break;
                    case 5:
                        task.Status = _generalMenu.ChooseStatus();
                        break;
                    case 6:
                        _taskManager.ChangeTask(task, index);
                        break;
                    case 0:
                        _renderer.Clear();
                        return;
                    default:
                        _renderer.Clear();
                        _renderer.ShowInvalidOptionMessage();
                        Console.ReadLine();
                        break;
                }
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