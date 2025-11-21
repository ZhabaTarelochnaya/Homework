using ConsoleTaskManager.Menu;

namespace ConsoleTaskManager;

class Program
{
    static void Main(string[] args)
    {
        var taskManager = new TaskManager();
        SetUpTestTasks(taskManager);
        
        var renderer = new ConsoleRenderer(taskManager);

        var generalMenu = new GeneralMenu(renderer);
        var addTaskMenu = new AddTaskMenu(taskManager, generalMenu);
        var deleteTaskMenu = new DeleteTaskMenu(renderer, taskManager);
        var editTaskMenu = new EditTaskMenu(renderer, taskManager, generalMenu);
        var viewTasksMenu = new ViewTasksMenu(renderer, generalMenu);
        
        while (true)
        {
            renderer.ShowMainMenu();
            if (int.TryParse(Console.ReadLine(), out var index))
            {
                switch (index)
                {
                    case 1:
                        addTaskMenu.Show();
                        break;
                    case 2:
                        viewTasksMenu.Show();
                        break;
                    case 3:
                        editTaskMenu.Show();
                        break;
                    case 4:
                        deleteTaskMenu.Show();
                        break;
                    case 0:
                        renderer.Clear();
                        return;
                    default:
                        renderer.Clear();
                        renderer.ShowInvalidOptionMessage();
                        Console.ReadLine();
                        break;
                }
            }
            else
            {
                renderer.Clear();
                renderer.ShowInvalidOptionMessage();
                Console.ReadLine();
            }
            renderer.Clear();
        }
    }
    static void SetUpTestTasks(TaskManager taskManager)
    {
        var description =
            $"Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut " +
            $"labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco " +
            $"laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in " +
            $"voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat " +
            $"non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";
        taskManager.AddTask(TaskBuilder.Empty()
            .Priority(Priority.Low)
            .Status(Status.New)
            .Category(Category.Home)
            .Name("Task1")
            .Description("description")
            .BuildData());
        taskManager.AddTask(TaskBuilder.Empty()
            .Priority(Priority.High)
            .Status(Status.InProgress)
            .Category(Category.Work)
            .Name("Task2")
            .Description(description)
            .BuildData());
    }
}