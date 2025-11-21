namespace ConsoleTaskManager;

public class TaskItem
{
    public string? Name { get; }
    public string? Description { get; }
    public Category Category { get; }
    public Priority Priority { get; }
    public Status Status { get; }
    public TaskItem(TaskData taskData)
    {
        Name = taskData.Name;
        Description = taskData.Description;
        Category = taskData.Category;
        Priority = taskData.Priority;
        Status = taskData.Status;
    }

    public override bool Equals(object? obj)
    {
        var task = obj as TaskItem;
        if (task == null)
        {
            return false;
        }
        if (Name == task.Name && Description == task.Description && Category == task.Category
            && Priority == task.Priority && Status == task.Status)
        {
            return true;
        }
        return false;
    }
}