using System.Text;

namespace ConsoleTaskManager;

public class ConsoleRenderer
{
    readonly TaskManager _taskManager;
    const int CharLimit = 60;
    List<TaskItem> _tasks = new();
    public ConsoleRenderer(TaskManager taskManager)
    {
        _taskManager = taskManager;
        UpdateTasks();
    }

    public void ShowMainMenu()
    {
        Console.WriteLine("1 - Add new task");
        Console.WriteLine("2 - View tasks");
        Console.WriteLine("3 - Edit task");
        Console.WriteLine("4 - Delete task");
        Console.WriteLine("0 - Exit");
    }
    public void ShowTask(int index)
    {
        CheckIndex(index);
        var task = _tasks[index];
        ShowTask(task, index);
    }
    public void ShowTask(TaskItem task, int index)
    {
        var description = task.Description == null ? "" : InsertNewLines(task.Description, CharLimit);
        Console.WriteLine($"{index + 1}: {task.Name}\n" +
                          $"    Description:\n" +
                          $"{description}\n" +
                          $"    Status: {task.Status}" +
                          $" | Priority: {task.Priority}" +
                          $" | Category: {task.Category}");
    }
    public void ShowTasks()
    {
        UpdateTasks();
        for (int i = 0; i < _tasks.Count; i++)
        {
            ShowTask(i);
            Console.WriteLine();
        }
    }

    public void ShowTasksByCategory(Category category)
    {
        foreach (var task in _taskManager.GetTasksByCategory(category))
        {
            ShowTask(GetIndex(task));
        }
    }

    public void ShowTasksByPriority(Priority priority)
    {
        foreach (var task in _taskManager.GetTasksByPriority(priority))
        {
            ShowTask(GetIndex(task));
        }
    }

    public void ShowTasksByStatus(Status status)
    {
        foreach (var task in _taskManager.GetTasksByStatus(status))
        {
            ShowTask(GetIndex(task));
        }
    }
    public void ShowViewMenu() => Console.WriteLine("1 - Filter by category\n" +
                                                    "2 - Filter by priority\n" +
                                                    "3 - Filter by status\n" +
                                                    "0 - Exit");
    public void ShowEditTaskMenu(TaskItem task, int index)
    {
        UpdateTasks();
        ShowTask(task, index);
        Console.WriteLine("\nChoose field to edit:\n" +
                          "1 - Name\n" +
                          "2 - Description\n" +
                          "3 - Category\n" +
                          "4 - Priority\n" +
                          "5 - Status\n" +
                          "6 - Save\n" +
                          "0 - Exit");
    }

    public void ShowEnterTaskNumber() => Console.WriteLine("Enter task number: ");
    public void UpdateTasks()
    {
        _tasks = _taskManager.GetTasks().ToList();
    }
    void CheckIndex(int index)
    {
        if (index < 0 || index >= _tasks.Count)
        {
            throw new IndexOutOfRangeException();
        }
    }
    public void ShowEnterNameMessage() => Console.WriteLine("Enter task name:");
    public void ShowEnterDescriptionMessage() => Console.WriteLine("Enter task description:");

    public void ShowChooseStatusMessage()
    {
        Console.WriteLine("Choose task status:\n" +
                          $"1 - {Status.New}\n" +
                          $"2 - {Status.InProgress}\n" +
                          $"3 - {Status.Done}");
    }
    public void ShowChooseCategoryMessage()
    {
        Console.WriteLine("Choose task category:\n" +
                          $"1 - {Category.Other}\n" +
                          $"2 - {Category.Home}\n" +
                          $"3 - {Category.Work}\n" +
                          $"4 - {Category.Study}");
    }
    public void ShowChoosePriorityMessage()
    {
        Console.WriteLine("Choose task priority:\n" +
                          $"1 - {Priority.Low}\n" +
                          $"2 - {Priority.Medium}\n" +
                          $"3 - {Priority.High}");
    }
    public void Clear() => Console.Clear();
    public void ShowInvalidOptionMessage() => Console.WriteLine("Invalid option");
    string InsertNewLines(string text, int maxCharsPerLine)
    {
        string[] words = text.Split(' ');
        StringBuilder sb = new StringBuilder();
        int currLength = 0;
        sb.Append("        ");
        foreach(string word in words)
        {
            if(currLength + word.Length + 1 < maxCharsPerLine) // +1 accounts for adding a space
            {
                sb.Append($"{word} ");
                currLength = sb.Length % maxCharsPerLine;
            }
            else
            {
                sb.Append($"\n        {word}");
                currLength = 0;
            }
        }
        return sb.ToString();
    }
    int GetIndex(TaskItem task)
    {
        var index = _tasks.IndexOf(task);
        return index;
    }
}