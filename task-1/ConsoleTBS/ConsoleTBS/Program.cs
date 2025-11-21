using ConsoleTBS.Effects;
using ConsoleTBS.PlayerActions;
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
        
        var renderer = new ConsoleRenderer(player);

        var attackAction = new AttackAction(player, renderer);
        var consumeAction = new ConsumeAction(player, renderer);
        var openShopAction = new OpenShopAction(shop, player, renderer);
        var switchConsumableAction = new SwitchConsumableAction(player, renderer);
        var switchWeaponAction = new SwitchWeaponAction(player, renderer);
        
        var mainMenu = new MainMenu(attackAction,  consumeAction, openShopAction, 
            switchConsumableAction, switchWeaponAction, renderer);
        
        while (true)
        {
            bool isExitPressed = mainMenu.Show(enemy);
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