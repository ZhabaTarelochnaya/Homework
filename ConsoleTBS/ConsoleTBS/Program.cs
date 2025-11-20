using ConsoleTBS.Effects;
using ConsoleTBS.Potions;
using ConsoleTBS.Weapons;
using ConsoleTBS.Weapons.Types;

namespace ConsoleTBS;

class Program
{
    static void Main(string[] args)
    {
        var rng = new Random();
        var player = CreatePlayer();
        var enemy = CreateEnemy(rng);
        var shop = CreateShop(player);
        var renderer = new ConsoleRenderer();
        while (true)
        {
            bool isExitPressed = OpenMenu(renderer, player, enemy, shop);
            if (isExitPressed) break;
            
            renderer.Clear();
            if (enemy.CurrentHealth == 0)
            {
                player.CoinsLeft += enemy.Reward;
                renderer.ShowEnemyDead(enemy);
                enemy = CreateEnemy(rng);
            }
            else
            {
                var damage = enemy.Attack(player);
                renderer.ShowAttackInfo(player, damage);
                if (player.CurrentHealth == 0)
                {
                    renderer.Clear();
                    renderer.ShowDefeat();
                    Console.ReadLine();
                    return;
                }
            }
            player.EffectProcessor.DisposeCompleted();
            
            Console.ReadLine();
            renderer.Clear();
        }
    }

    static bool OpenMenu(ConsoleRenderer renderer, Player player, Enemy enemy, Shop shop)
    {
        while (true)
        {
            renderer.ShowOptions();
            if (int.TryParse(Console.ReadLine(), out var option))
            {
                renderer.Clear();
                switch (option)
                {
                    case 1:
                        player.EffectProcessor.ApplyEffects();
                        var damage = player.Attack(enemy);
                        renderer.ShowAttackInfo(enemy, damage);
                        Console.ReadLine();
                        return false;
                    case 2:
                        if (player.TryConsumeCurrent(out IConsumable? consumable))
                        {
                            player.EffectProcessor.ApplyEffects();
                            renderer.ConsumablesRenderer.ShowConsumedMessage(consumable);
                            Console.ReadLine();
                            return false;
                        }
                        renderer.ConsumablesRenderer.ShowConsumableNotEquipped();
                        Console.ReadLine();
                        break;
                    case 3:
                        SwitchWeapon(player, renderer);
                        break;
                    case 4:
                        SwitchConsumable(player, renderer);
                        break;
                    case 5:
                        renderer.ShowStatus(player);
                        Console.ReadLine();
                        break;
                    case 6:
                        OpenShop(shop, player, renderer);
                        break;
                    case 0:
                        return true;
                    default:
                        renderer.Clear();
                        renderer.ShowInvalidInput();
                        Console.ReadLine();
                        break;
                }
            }
            else
            {
                renderer.Clear();
                renderer.ShowInvalidInput();
                Console.ReadLine();
            }
            renderer.Clear();
        }
    }

    static void SwitchWeapon(Player player, ConsoleRenderer renderer)
    {
        renderer.Clear();
        if (!player.Weapons.Any())
        {
            renderer.ShowEmpty();
            Console.ReadLine();
            return;
        }
        
        renderer.WeaponsRenderer.ShowAvailableWeapons(player.Weapons);
        Console.WriteLine("0 - Exit");
        if (int.TryParse(Console.ReadLine(), out var index))
        {
            if (index == 0) return;
            renderer.Clear();
            index--;
            if (index >= player.Weapons.Count())
            {
                renderer.ShowInvalidInput();
                Console.ReadLine();
                return;
            }
            player.SwitchCurrentWeapon(index);
            renderer.WeaponsRenderer.ShowCurrentWeapon(player);
        }
        else
        {
            renderer.Clear();
            renderer.ShowInvalidInput();
        }
        Console.ReadLine();
        renderer.Clear();
    }

    static void SwitchConsumable(Player player, ConsoleRenderer renderer)
    {
        renderer.Clear();
        if (!player.Consumables.Any())
        {
            renderer.ShowEmpty();
            Console.ReadLine();
            return;
        }
        
        renderer.ConsumablesRenderer.ShowAvailableConsumables(player.Consumables);
        Console.WriteLine("0 - Exit");
        if (int.TryParse(Console.ReadLine(), out var index))
        {
            if (index == 0) return;
            renderer.Clear();
            index--;
            if (index >= player.Consumables.Count())
            {
                renderer.ShowInvalidInput();
                Console.ReadLine();
                return;
            }
            player.SwitchCurrentConsumable(index);
            renderer.ConsumablesRenderer.ShowCurrentConsumable(player);
        }
        else
        {
            renderer.Clear();
            renderer.ShowInvalidInput();
        }
        Console.ReadLine();
        renderer.Clear();
    }

    static void OpenShop(Shop shop, Player player,  ConsoleRenderer renderer)
    {
        renderer.Clear();
        if (!shop.Items.Any())
        {
            renderer.ShowEmpty();
            Console.ReadLine();
            return;
        }
        renderer.ShopRenderer.ShowShop(shop, player);
        Console.WriteLine("0 - Exit");
        if (int.TryParse(Console.ReadLine(), out var index))
        {
            if (index == 0) return;
            renderer.Clear();
            index--;
            if (index >= shop.Items.Count())
            {
                renderer.ShowInvalidInput();
                Console.ReadLine();
                return;
            }

            if (shop.TryBuyItem(index, out var item))
            {
                renderer.ShopRenderer.ShowPurchase(item);
            }
            else
            {
                renderer.ShopRenderer.ShowNotEnoughMoney(item, player);
            }
        }
        else
        {
            renderer.Clear();
            renderer.ShowInvalidInput();
        }
        Console.ReadLine();
    }
    static Enemy CreateEnemy(Random rng)
    {
        var character = new Character(rng.Next(10, 31), rng.Next(0,3));
        var enemy = new Enemy(character, rng.Next(3, 7));
        int weaponIndex = rng.Next(0, 2);
        IWeapon weapon;
        switch (weaponIndex)
        {
            case 0:
                weapon = new Sword(3, 10);
                break;
            case 1:
                weapon = new Daggers(1, 10);
                break;
            default:
                weapon = new Sword(3, 10);
                break;
        }
        enemy.EquipWeapon(weapon);
        return enemy;
    }

    static Player CreatePlayer()
    {
        var character = new Character(40, 2);
        var player = new Player(character);

        player.AddWeapon(new Sword(4, 10));
        player.AddWeapon(new Daggers(2, 10));
        
        player.AddConsumable(new HealingPotion(20, 10));
        player.AddConsumable(new DamageUpPotion(5, 3, 15));
        player.CoinsLeft += 100;
        return player;
    }

    static Shop CreateShop(Player player)
    {
        var shop = new Shop(player);
        shop.AddItem(new Sword(6, 10));
        shop.AddItem(new Daggers(5, 15));
        shop.AddItem(new HealingPotion(20, 10));
        shop.AddItem(new DamageUpPotion(5, 3, 20));
        return shop;
    }
}