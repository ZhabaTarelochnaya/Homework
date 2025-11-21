namespace ConsoleTBS.PlayerActions;

public class OpenShopAction
{
    readonly Shop _shop;
    readonly Player _player;
    readonly ConsoleRenderer _renderer;

    public OpenShopAction(Shop shop, Player player,  ConsoleRenderer renderer)
    {
        _shop = shop;
        _player = player;
        _renderer = renderer;
    }
    public void Do()
    {
        _renderer.Clear();
        if (!_shop.Items.Any())
        {
            _renderer.ShowEmpty();
            Console.ReadLine();
            return;
        }
        _renderer.ShopRenderer.ShowShop(_shop, _player);
        Console.WriteLine("0 - Exit");
        if (int.TryParse(Console.ReadLine(), out var index))
        {
            if (index == 0) return;
            _renderer.Clear();
            index--;
            if (index >= _shop.Items.Count())
            {
                _renderer.ShowInvalidInput();
                Console.ReadLine();
                return;
            }

            if (_shop.TryBuyItem(index, out var item))
            {
                _renderer.ShopRenderer.ShowPurchase(item);
            }
            else
            {
                _renderer.ShopRenderer.ShowNotEnoughMoney(item, _player);
            }
        }
        else
        {
            _renderer.Clear();
            _renderer.ShowInvalidInput();
        }
        Console.ReadLine();
    }
}