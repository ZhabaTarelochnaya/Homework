namespace ConsoleTaskManager;

public class Task
{
    public string? Name { get; }
    public string? Description { get; }
    public Category Category { get; }
    public Priority Priority { get; }
    public Status Status { get; }
    public Task(TaskData taskData)
    {
        Name = taskData.Name;
        Description = taskData.Description;
        Category = taskData.Category;
        Priority = taskData.Priority;
        Status = taskData.Status;
    }
}