namespace ConsoleTaskManager;

public class TaskManager
{
    List<TaskData> _tasks = new ();
    public void AddTask(TaskData taskData) => _tasks.Add(taskData);
    public void RemoveTask(TaskData taskData) => _tasks.Remove(taskData);
    public void ChangeTask(TaskData taskData, int index) => _tasks[index] = taskData;
    public bool TryGetTask(int index, out TaskData taskData)
    {
        if (_tasks.Count <= index || index < 0)
        {
            taskData = null;
            return false;
        }
        taskData = _tasks[index];
        return true;
    }
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