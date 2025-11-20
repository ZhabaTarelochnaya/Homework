using ConsoleTBS.Potions;

namespace ConsoleTBS;

public class ConsumablesRenderer
{
    public void ShowConsumableInfo(IConsumable consumable)
    {
        Console.WriteLine($"{consumable.Name}: {consumable.GetDescription()}");
    } 
    public void ShowConsumedMessage(IConsumable consumable)
    {
        Console.WriteLine("Consumed: " + consumable.Name);
    }

    public void ShowCurrentConsumable(ICharacter character)
    {
        if (character.CurrentConsumable != null)
        {
            Console.Write($"Equipped Potion: ");
            ShowConsumableInfo(character.CurrentConsumable);
        }
        else
        {
            Console.WriteLine($"Equipped Potion: none");
        }
        
    }
    public void ShowConsumableNotEquipped()
    {
        Console.WriteLine("Potion not equipped");
    }
    public void ShowAvailableConsumables(IEnumerable<IConsumable> consumables)
    {
        if (!consumables.Any())
        {
            Console.WriteLine("No potions left");
            return;
        }
        Console.WriteLine($"Available potions:");
        int i = 1;
        foreach (var consumable in consumables)
        {
            Console.Write($"{i} - ");
            ShowConsumableInfo(consumable);
            i++;
        }
    }
}