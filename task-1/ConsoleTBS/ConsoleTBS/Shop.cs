using ConsoleTBS.Potions;
using ConsoleTBS.Weapons;

namespace ConsoleTBS;

public class Shop
{
    readonly Player _player;
    List<Item> items = new ();
    public IEnumerable<Item> Items => items;

    public Shop(Player player)
    {
        _player = player;
    }
    public void AddItem(Item item)
    {
        items.Add(item);
    }

    public bool TryBuyItem(int index, out Item? item)
    {
        item = items[index];
        if (_player.CoinsLeft < item.Price)
        {
            return false;
        }
        _player.CoinsLeft -= item.Price;
        item.Use(_player);
        items.RemoveAt(index);
        return true;
    }
}