using ConsoleTBS.Effects;
using ConsoleTBS.Potions;
using ConsoleTBS.Weapons;

namespace ConsoleTBS;

class Program
{
    static void Main(string[] args)
    {
        var rng = new Random();
        var player = CreatePlayer();
        var enemy = CreateEnemy();
        var renderer = new ConsoleRenderer();
        bool isExitPressed = false;
        while (!isExitPressed)
        {
            isExitPressed = OpenMenu(renderer, player, enemy);
            renderer.Clear();
            player.EffectProcessor.ApplyEffects();
            var damage = enemy.Attack(player);
            renderer.ShowAttackInfo(player, damage);
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
                        var damage = player.Attack(enemy);
                        renderer.ShowAttackInfo(enemy, damage);
                        Console.ReadLine();
                        return false;
                    case 2:
                        if (player.TryConsumeCurrent(out IConsumable? consumable))
                        {
                            renderer.ShowConsumedMessage(consumable);
                        }
                        else
                        {
                            renderer.ShowConsumableNotEquipped();
                        }
                        Console.ReadLine();
                        return false;
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
    static Enemy CreateEnemy()
    {
        var character = new Character(20, 2);
        var enemy = new Enemy(character);

        Sword sword = new(4);
        enemy.EquipWeapon(sword);
        return enemy;
    }

    static Player CreatePlayer()
    {
        var character = new Character(40, 2);
        var player = new Player(character);

        Sword sword = new(4);
        player.EquipWeapon(sword);
        
        player.AddConsumable(new HealingPotion(20, 10));
        player.AddConsumable(new DamageUpPotion(5, 3, 15));
        return player;
    }
}