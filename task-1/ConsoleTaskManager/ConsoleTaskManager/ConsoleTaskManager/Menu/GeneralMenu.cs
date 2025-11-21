namespace ConsoleTaskManager.Menu;

public class GeneralMenu
{
    readonly ConsoleRenderer _renderer;

    public GeneralMenu(ConsoleRenderer renderer)
    {
        _renderer = renderer;
    }
    public string? ChooseName()
    {
        _renderer.Clear();
        _renderer.ShowEnterNameMessage();
        return Console.ReadLine();
    }
    public string? ChooseDescription()
    {
        _renderer.Clear();
        _renderer.ShowEnterDescriptionMessage();
        return Console.ReadLine();
    }
    public Category ChooseCategory()
    {
        while (true)
        {
            _renderer.Clear();
            _renderer.ShowChooseCategoryMessage();
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
                        _renderer.Clear();
                        _renderer.ShowInvalidOptionMessage();
                        Console.ReadLine();
                        break;
                }
            }
            else
            {
                _renderer.Clear();
                _renderer.ShowInvalidOptionMessage();
                Console.ReadLine();
            }
        }
    }
    public Priority ChoosePriority()
    {
        while (true)
        {
            _renderer.Clear();
            _renderer.ShowChoosePriorityMessage();
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
                        _renderer.Clear();
                        _renderer.ShowInvalidOptionMessage();
                        Console.ReadLine();
                        break;
                }
            }
            else
            {
                _renderer.Clear();
                _renderer.ShowInvalidOptionMessage();
                Console.ReadLine();
            }
        }
    }
    public Status ChooseStatus()
    {
        while (true)
        {
            _renderer.Clear();
            _renderer.ShowChooseStatusMessage();
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
                        _renderer.Clear();
                        _renderer.ShowInvalidOptionMessage();
                        Console.ReadLine();
                        break;
                }
            }
            else
            {
                _renderer.Clear();
                _renderer.ShowInvalidOptionMessage();
                Console.ReadLine();
            }
        }
    }
}