namespace ConsoleTaskManager;

public class TaskBuilder
{
    TaskData _taskData =  new TaskData();
    TaskBuilder() {}
    TaskBuilder(TaskData taskData) => _taskData = taskData;
    TaskBuilder(Task task)
    {
        _taskData.Name = task.Name;
        _taskData.Description = task.Description;
        _taskData.Category  = task.Category;
        _taskData.Status = task.Status;
        _taskData.Priority = task.Priority;
    }
    public static TaskBuilder Empty() => new TaskBuilder();
    public static TaskBuilder FromTask(TaskData task) => new(task);
    public static TaskBuilder FromTask(Task task) => new(task);
    public TaskBuilder Name(string name)
    {
        _taskData.Name = name;
        return this;
    }
    public TaskBuilder Description(string description)
    {
        _taskData.Description = description;
        return this;
    }
    public TaskBuilder Category(Category category)
    {
        _taskData.Category = category;
        return this;
    }
    public TaskBuilder Priority(Priority priority)
    {
        _taskData.Priority = priority;
        return this;
    }
    public TaskBuilder Status(Status status)
    {
        _taskData.Status = status;
        return this;
    }
    public Task Build() => new(_taskData);
}