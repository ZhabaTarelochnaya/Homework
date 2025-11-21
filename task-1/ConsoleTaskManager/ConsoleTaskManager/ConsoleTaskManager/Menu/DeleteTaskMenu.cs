namespace ConsoleTaskManager.Menu;

public class DeleteTaskMenu
{
    readonly ConsoleRenderer _renderer;
    readonly TaskManager _taskManager;

    public DeleteTaskMenu(ConsoleRenderer renderer, TaskManager taskManager)
    {
        _renderer = renderer;
        _taskManager = taskManager;
    }
    public void Show()
    {
        _renderer.Clear();
        _renderer.ShowEnterTaskNumber();
        if (int.TryParse(Console.ReadLine(), out var index))
        {
            if (_taskManager.TryGetTask(--index, out var task))
            {
                _taskManager.RemoveTask(task);
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
}