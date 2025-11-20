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
                          "6 - Open shop\n" +
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
        ShowCoins(player);
        Console.WriteLine();
        ShowCurrentWeapon(player);
        ShowAvailableWeapons(player.Weapons);
        Console.WriteLine();
        ShowCurrentConsumable(player);
        ShowAvailableConsumables(player.Consumables);
    }

    public void ShowShop(Shop shop, Player player)
    {
        int i = 1;
        ShowCoins(player);
        foreach (var item in shop.Items)
        {
            if (item is IWeapon weapon)
            {
                Console.Write($"{i} ({item.Price} coins) - ");
                ShowWeaponInfo(weapon);
            }
            else if (item is IConsumable consumable)
            {
                Console.Write($"{i} ({item.Price} coins) - ");
                ShowConsumableInfo(consumable);
            }
            else
            {
                throw new Exception("Unknown shop item type");
            }
            i++;
        }
    }
    public void ShowPurchase(Item item)
    {
        Console.WriteLine($"Bought item: {item.Name}");
    }
    public void ShowNotEnoughMoney(Item item, Player player)
    {
        Console.WriteLine($"Not enough money: {player.CoinsLeft} /  {item.Price}");
    }
    public void ShowShopEmpty()
    {
        Console.WriteLine("Shop is empty");
    }

    public void ShowCoins(Player player)
    {
        Console.WriteLine($"Coins: {player.CoinsLeft}");
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
            Console.Write("Equipped Weapon: ");
            ShowWeaponInfo(weaponWielder.Weapon);
        }
        else
        {
            Console.WriteLine($"Equipped Weapon: none");
        }
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