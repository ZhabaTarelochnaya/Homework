using ConsoleTBS.Effects;
using ConsoleTBS.Potions;
using ConsoleTBS.Weapons;

namespace ConsoleTBS;

public class ConsoleRenderer
{
    readonly Player _player;
    public ConsumablesRenderer ConsumablesRenderer { get; }
    public WeaponsRenderer WeaponsRenderer { get; }
    public ShopRenderer ShopRenderer { get; }

    public ConsoleRenderer(Player player)
    {
        _player = player;
        ConsumablesRenderer = new ConsumablesRenderer();
        WeaponsRenderer = new WeaponsRenderer();
        ShopRenderer = new ShopRenderer(WeaponsRenderer, ConsumablesRenderer);
    }
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
    public void ShowPlayerStatus()
    {
        Console.WriteLine($"Health: {_player.CurrentHealth} / {_player.MaxHealth}\n" +
                          $"Damage: {_player.BaseDamage} + {_player.Weapon.GetDamageFormula()}\n" +
                          $"Current Effects:");
        foreach (var effect in _player.EffectProcessor.CurrentEffects)
        {
            Console.Write("\t");
            ShowEffectInfo(effect);
        } 
        Console.WriteLine();
        ShopRenderer.ShowCoins(_player);
        Console.WriteLine();
        WeaponsRenderer.ShowCurrentWeapon(_player);
        WeaponsRenderer.ShowAvailableWeapons(_player.Weapons);
        Console.WriteLine();
        ConsumablesRenderer.ShowCurrentConsumable(_player);
        ConsumablesRenderer.ShowAvailableConsumables(_player.Consumables);
    }
    public void ShowAttackInfo(ICharacter target, int damage)
    {
        Console.WriteLine($"{target.Name} took {damage} damage");
    }
    public void ShowInvalidInput() => Console.WriteLine("Invalid input");
    
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
    public void ShowEmpty()
    {
        Console.WriteLine("Empty");
    }
    public void Clear() => Console.Clear();
}