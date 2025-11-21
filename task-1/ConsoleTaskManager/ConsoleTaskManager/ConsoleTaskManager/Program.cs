namespace ConsoleTaskManager;

class Program
{
    static void Main(string[] args)
    {
        var taskManager = new TaskManager();
        var renderer = new ConsoleRenderer(taskManager);
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

        while (true)
        {
            renderer.ShowMainMenu();
            if (int.TryParse(Console.ReadLine(), out var index))
            {
                switch (index)
                {
                    case 1:
                        AddTask(renderer, taskManager);
                        break;
                    case 2:
                        ViewTasks(renderer);
                        break;
                    case 3:
                        EditTask(renderer, taskManager);
                        break;
                    case 4:
                        DeleteTask(renderer, taskManager);
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
    static void ViewTasks(ConsoleRenderer renderer)
    {
        while (true)
        {
            renderer.Clear();
            renderer.ShowTasks();
            renderer.ShowViewMenu();
            if (int.TryParse(Console.ReadLine(), out var index))
            {
                renderer.Clear();
                switch (index)
                {
                    case 1:
                        var category = ChooseCategory(renderer);
                        renderer.Clear();
                        renderer.ShowTasksByCategory(category);
                        break;
                    case 2:
                        var priority = ChoosePriority(renderer);
                        renderer.Clear();
                        renderer.ShowTasksByPriority(priority);
                        break;
                    case 3:
                        var status = ChooseStatus(renderer);
                        renderer.Clear();
                        renderer.ShowTasksByStatus(status);
                        break;
                    case 0:
                        renderer.Clear();
                        return;
                    default:
                        renderer.Clear();
                        renderer.ShowInvalidOptionMessage();
                        break;
                }

                Console.ReadLine();
            }
            else
            {
                renderer.Clear();
                renderer.ShowInvalidOptionMessage();
                Console.ReadLine();
            }
        }
    }
    static void DeleteTask(ConsoleRenderer renderer, TaskManager taskManager)
    {
        renderer.Clear();
        renderer.ShowEnterTaskNumber();
        if (int.TryParse(Console.ReadLine(), out var index))
        {
            if (taskManager.TryGetTask(--index, out var task))
            {
                taskManager.RemoveTask(task);
            }
            else
            {
                renderer.Clear();
                renderer.ShowInvalidOptionMessage();
                Console.ReadLine();
            }
        }
        else
        {
            renderer.Clear();
            renderer.ShowInvalidOptionMessage();
            Console.ReadLine();
        }
    }
    static void EditTask(ConsoleRenderer renderer, TaskManager taskManager)
    {
        renderer.Clear();
        renderer.ShowEnterTaskNumber();
        if (int.TryParse(Console.ReadLine(), out var index))
        {
            if (taskManager.TryGetTask(--index, out var task))
            {
                task = TaskBuilder.FromTask(task).BuildData();
                EditTaskMenu(renderer, taskManager, task, index);
            }
            else
            {
                renderer.Clear();
                renderer.ShowInvalidOptionMessage();
                Console.ReadLine();
            }
        }
        else
        {
            renderer.Clear();
            renderer.ShowInvalidOptionMessage();
            Console.ReadLine();
        }
    }

    static void EditTaskMenu(ConsoleRenderer renderer, TaskManager taskManager, TaskData task, int index)
    {
        while (true)
        {
            renderer.Clear();
            var taskItem = TaskBuilder.FromTask(task).Build();
            renderer.ShowEditTaskMenu(taskItem, index);
            if (int.TryParse(Console.ReadLine(), out var input))
            {
                renderer.Clear();
                switch (input)
                {
                    case 1:
                        task.Name = ChooseName(renderer);
                        break;
                    case 2:
                        task.Description = ChooseDescription(renderer);
                        break;
                    case 3:
                        task.Category = ChooseCategory(renderer);
                        break;
                    case 4:
                        task.Priority = ChoosePriority(renderer);
                        break;
                    case 5:
                        task.Status = ChooseStatus(renderer);
                        break;
                    case 6:
                        taskManager.ChangeTask(task, index);
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
        }
    }
    static void AddTask(ConsoleRenderer renderer, TaskManager taskManager)
    {
        taskManager.AddTask(TaskBuilder.Empty()
            .Name(ChooseName(renderer))
            .Description(ChooseDescription(renderer))
            .Category(ChooseCategory(renderer))
            .Priority(ChoosePriority(renderer))
            .Status(ChooseStatus(renderer))
            .BuildData());
    }
    static string? ChooseName(ConsoleRenderer renderer)
    {
        renderer.Clear();
        renderer.ShowEnterNameMessage();
        return Console.ReadLine();
    }
    static string? ChooseDescription(ConsoleRenderer renderer)
    {
        renderer.Clear();
        renderer.ShowEnterDescriptionMessage();
        return Console.ReadLine();
    }
    static Category ChooseCategory(ConsoleRenderer renderer)
    {
        while (true)
        {
            renderer.Clear();
            renderer.ShowChooseCategoryMessage();
            if (int.TryParse(Console.ReadLine(), out var index))
            {
                switch (index)
                {
                    case 1:
                        return Category.Other;
                    case 2:
                        return Category.Home;
                    case 3:
                        return Category.Work;
                    case 4:
                        return Category.Study;
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
        }
    }
    static Priority ChoosePriority(ConsoleRenderer renderer)
    {
        while (true)
        {
            renderer.Clear();
            renderer.ShowChoosePriorityMessage();
            if (int.TryParse(Console.ReadLine(), out var index))
            {
                switch (index)
                {
                    case 1:
                        return Priority.Low;
                    case 2:
                        return Priority.Medium;
                    case 3:
                        return Priority.High;
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
        }
    }
    static Status ChooseStatus(ConsoleRenderer renderer)
    {
        while (true)
        {
            renderer.Clear();
            renderer.ShowChooseStatusMessage();
            if (int.TryParse(Console.ReadLine(), out var index))
            {
                switch (index)
                {
                    case 1:
                        return Status.New;
                    case 2:
                        return Status.InProgress;
                    case 3:
                        return Status.Done;
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
        }
    }
   
}