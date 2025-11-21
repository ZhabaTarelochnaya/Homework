namespace ConsoleTaskManager;

public class TaskBuilder
{
    readonly TaskData _taskData =  new TaskData();
    TaskBuilder() {}

    TaskBuilder(TaskData taskData)
    {
        _taskData.Name = taskData.Name;
        _taskData.Description = taskData.Description;
        _taskData.Category  = taskData.Category;
        _taskData.Status = taskData.Status;
        _taskData.Priority = taskData.Priority;
    }
    TaskBuilder(TaskItem taskItem)
    {
        _taskData.Name = taskItem.Name;
        _taskData.Description = taskItem.Description;
        _taskData.Category  = taskItem.Category;
        _taskData.Status = taskItem.Status;
        _taskData.Priority = taskItem.Priority;
    }
    public static TaskBuilder Empty() => new TaskBuilder();
    public static TaskBuilder FromTask(TaskData task) => new(task);
    public static TaskBuilder FromTask(TaskItem taskItem) => new(taskItem);
    public TaskBuilder Name(string? name)
    {
        _taskData.Name = name;
        return this;
    }
    public TaskBuilder Description(string? description)
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
    public TaskItem Build() => new(_taskData);
    public TaskData BuildData() => _taskData;
}