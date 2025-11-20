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
        var renderer = new ConsoleRenderer();
        
        while (true)
        {
            bool isExitPressed = OpenMenu(renderer, player, enemy);
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

    static bool OpenMenu(ConsoleRenderer renderer, Player player, Enemy enemy)
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
                            renderer.ShowConsumedMessage(consumable);
                            Console.ReadLine();
                            return false;
                        }
                        renderer.ShowConsumableNotEquipped();
                        Console.ReadLine();
                        break;
                    case 3:
                        break;
                    case 4:
                        SwitchConsumable(player, renderer);
                        break;
                    case 5:
                        renderer.ShowStatus(player);
                        Console.ReadLine();
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

    static void SwitchConsumable(Player player, ConsoleRenderer renderer)
    {
        renderer.Clear();
        renderer.ShowAvailableConsumables(player.Consumables);
        if (!player.Consumables.Any())
        {
            Console.ReadLine();
            return;
        }
        if (int.TryParse(Console.ReadLine(), out var index))
        {
            renderer.Clear();
            index--;
            if (index >= player.Consumables.Count())
            {
                renderer.ShowInvalidInput();
                Console.ReadLine();
                return;
            }
            var consumable = player.SwitchCurrentConsumable(index);
            renderer.ShowConsumableEquipped(consumable);
        }
        else
        {
            renderer.Clear();
            renderer.ShowInvalidInput();
        }
        Console.ReadLine();
        renderer.Clear();
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

        Sword sword = new(4, 10);
        player.EquipWeapon(sword);
        
        player.AddConsumable(new HealingPotion(20, 10));
        player.AddConsumable(new DamageUpPotion(5, 3, 15));
        return player;
    }
}