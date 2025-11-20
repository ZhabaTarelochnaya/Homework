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

    public void ShowStatus(Player player)
    {
        Console.WriteLine($"Health: {player.CurrentHealth} / {player.MaxHealth}\n" +
                          $"Damage: {player.BaseDamage} + {player.Weapon.GetDamageFormula()}\n" +
                          $"Current Effects:");
        foreach (var effect in player.EffectProcessor.CurrentEffects)
        {
            Console.Write("\t");
            ShowEffectInfo(effect);
        } 
        Console.WriteLine();
        Console.WriteLine($"Coins: {player.CoinsLeft}");
        Console.WriteLine();
        ShowCurrentWeapon(player);
        ShowAvailableWeapons(player.Weapons);
        Console.WriteLine();
        ShowCurrentConsumable(player);
        ShowAvailableConsumables(player.Consumables);
    }
    public void ShowAttackInfo(ICharacter target, int damage)
    {
        Console.WriteLine($"{target.Name} took {damage} damage");
    }
    public void ShowCurrentWeapon(IWeaponWielder weaponWielder)
    {
        string equippedWeapon;
        if (weaponWielder.Weapon != null)
        {
            equippedWeapon = weaponWielder.Weapon.Name.ToString();
        }
        else
        {
            equippedWeapon = "none";
        }
        Console.WriteLine($"Equipped Weapon: {equippedWeapon}");
    }
    public void ShowAvailableWeapons(IEnumerable<IWeapon> weapons)
    {
        if (!weapons.Any())
        {
            Console.WriteLine("No weapons left");
            return;
        }
        Console.WriteLine($"Available weapons:");
        int i = 1;
        foreach (var weapon in weapons)
        {
            Console.Write($"{i} - ");
            ShowWeaponInfo(weapon);
            i++;
        }
    }
    public void ShowWeaponInfo(IWeapon weapon)
    {
        Console.WriteLine($"{weapon.Name}: {weapon.GetDamageFormula()} damage.");
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
        Console.WriteLine($"Equipped Potion: {equippedPotion}");
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
    public void ShowEnemyDead(IEnemy enemy)
    {
        Console.WriteLine($"Enemy killed. Reward: {enemy.Reward} coins.");
    }
    public void ShowDefeat()
    {
        Console.WriteLine("You are defeated.");
    }
    public void Clear() => Console.Clear();


   
}