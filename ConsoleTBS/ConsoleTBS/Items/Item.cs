namespace ConsoleTBS;

public abstract class Item
{
    public abstract ItemName Name { get; }
    public int Price { get; private set; }

    public Item(int price)
    {
        Price = price;
    }
    
    public abstract void Use(ICharacter character);
}