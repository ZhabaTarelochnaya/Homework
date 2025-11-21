namespace ConsoleTaskManager.Menu;

public class AddTaskMenu
{
    readonly TaskManager _taskManager;
    readonly GeneralMenu _generalMenu;

    public AddTaskMenu(TaskManager taskManager, GeneralMenu generalMenu)
    {
        _taskManager = taskManager;
        _generalMenu = generalMenu;
    }
    public void Show()
    {
        _taskManager.AddTask(TaskBuilder.Empty()
            .Name(_generalMenu.ChooseName())
            .Description(_generalMenu.ChooseDescription())
            .Category(_generalMenu.ChooseCategory())
            .Priority(_generalMenu.ChoosePriority())
            .Status(_generalMenu.ChooseStatus())
            .BuildData());
    }
}