namespace ConsoleTaskManager;

public class TaskManager
{
    List<TaskData> _tasks = new ();
    public void ChangeStatus(int index, Status status)
    {
        CheckIndex(index);
        _tasks[index].Status = status;
    }
    public void ChangePriority(int index, Priority priority)
    {
        CheckIndex(index);
        _tasks[index].Priority = priority;
    }
    public void ChangeCategory(int index, Category category)
    {
        CheckIndex(index);
        _tasks[index].Category = category;
    }
    public void ChangeName(int index, string? name)
    {
        CheckIndex(index);
        _tasks[index].Name = name;
    }
    public void ChangeDescription(int index, string? description)
    {
        CheckIndex(index);
        _tasks[index].Description = description;
    }
    public void AddTask(TaskData taskData) => _tasks.Add(taskData);
    public IEnumerable<TaskItem> GetTasks()
    {
        var tasks = _tasks.Select(t => TaskBuilder.FromTask(t).Build());
        return tasks;
    }
    public IEnumerable<TaskItem> GetTasksByCategory(Category category)
    {
        var tasks = _tasks.Where(t => t.Category == category)
            .Select(t => TaskBuilder.FromTask(t).Build());
        return tasks;
    }
    public IEnumerable<TaskItem> GetTasksByPriority(Priority priority)
    {
        var tasks = _tasks.Where(t => t.Priority == priority)
            .Select(t => TaskBuilder.FromTask(t).Build());
        return tasks;
    }
    public IEnumerable<TaskItem> GetTasksByStatus(Status status)
    {
        var tasks = _tasks.Where(t => t.Status == status)
            .Select(t => TaskBuilder.FromTask(t).Build());
        return tasks;
    }
    void CheckIndex(int index)
    {
        if (index < 0 || index >= _tasks.Count)
        {
            throw new IndexOutOfRangeException();
        }
    }
}