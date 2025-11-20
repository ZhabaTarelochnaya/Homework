using ConsoleTBS.Effects;
using ConsoleTBS.Potions;
using ConsoleTBS.Weapons;

namespace ConsoleTBS;

public class ConsoleRenderer
{
    public void ShowOptions()
    {
        Console.WriteLine("Choose action: \n" +
                          "1 - Attack\n" +
                          "2 - Use potion\n" +
                          "3 - Equip Weapon\n" +
                          "4 - Equip Potion\n" +
                          "5 - Show Status\n" +
                          "0 - Exit");
    }

    public void ShowStatus(ICharacter character)
    {
        Console.WriteLine($"Health: {character.CurrentHealth} / {character.MaxHealth}\n" +
                          $"Damage: {character.BaseDamage} + {character.Weapon.GetDamageFormula()}\n" +
                          $"Current Effects:\n");
        foreach (var effect in character.EffectProcessor.CurrentEffects)
        {
            Console.Write("\t");
            ShowEffectInfo(effect);
        } 
        ShowWeaponInfo(character.Weapon);
        ShowCurrentConsumable(character);
        ShowAvailableConsumables(character.Consumables);
    }
    public void ShowAttackInfo(ICharacter target, int damage)
    {
        Console.WriteLine($"{target.Name} took {damage} damage");
    }
    public void ShowWeaponInfo(IWeapon weapon)
    {
        Console.WriteLine($"Equipped Weapon: {weapon.Name} ({weapon.GetDamageFormula()})");
    }

   
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
        string equippedPotion;
        if (character.CurrentConsumable != null)
        {
            equippedPotion = character.CurrentConsumable.Name.ToString();
        }
        else
        {
            equippedPotion = "none";
        }
        Console.WriteLine($"Equipped Potion: {equippedPotion}\n");
    }
    public void ShowConsumableEquipped(IConsumable consumable)
    {
        Console.WriteLine("Equiped: " + consumable.Name);
    }
    public void ShowConsumableNotEquipped()
    {
        Console.WriteLine("Potion not equipped");
    }
    public void ShowInvalidInput() => Console.WriteLine("Invalid input");
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
    public void ShowEffectInfo(IEffect effect)
    {
        Console.WriteLine($"{effect.Name}: {effect.GetDescription()} Turns left: {effect.TurnsLeft}");
    }
    public void Clear() => Console.Clear();
}