using ConsoleTBS.Potions;
using ConsoleTBS.Weapons;

namespace ConsoleTBS;

public class ShopRenderer
{
    readonly WeaponsRenderer _weaponsRenderer;
    readonly ConsumablesRenderer _consumablesRenderer;

    public ShopRenderer(WeaponsRenderer weaponsRenderer, ConsumablesRenderer consumablesRenderer)
    {
        _weaponsRenderer = weaponsRenderer;
        _consumablesRenderer = consumablesRenderer;
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
                _weaponsRenderer.ShowWeaponInfo(weapon);
            }
            else if (item is IConsumable consumable)
            {
                Console.Write($"{i} ({item.Price} coins) - ");
                _consumablesRenderer.ShowConsumableInfo(consumable);
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
}