using System.Text;

namespace ConsoleTaskManager;

public class TaskRenderer
{
    readonly TaskManager _taskManager;
    const int CharLimit = 60;
    List<Task> _tasks = new ();

    public TaskRenderer(TaskManager taskManager)
    {
        _taskManager = taskManager;
        UpdateTasks();
    }

    public void ShowTask(int index)
    {
        CheckIndex(index);
        var task = _tasks[index];
        var description = task.Description == null ? "" : InsertNewLines(task.Description, CharLimit);
        Console.WriteLine($"{index + 1}: {task.Name}\n" +
                          $"Description:\n" +
                          $"{description}\n" +
                          $"Status: {task.Status}\n" +
                          $"Priority: {task.Priority}\n" +
                          $"Category: {task.Category}\n");
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

    public string InsertNewLines(string text, int maxCharsPerLine)
    {
        string[] words = text.Split(' ');
        StringBuilder sb = new StringBuilder();
        int currLength = 0;
        foreach(string word in words)
        {
            if(currLength + word.Length + 1 < maxCharsPerLine) // +1 accounts for adding a space
            {
                sb.Append($" {word}");
                currLength = sb.Length % maxCharsPerLine;
            }
            else
            {
                sb.Append($"\n{word}");
                currLength = 0;
            }
        }
        return sb.ToString();
    }
}