namespace ConsoleTBS.Weapons;

public interface IWeapon
{
    public ItemName Name { get; }
    public int GetDamage();
    public string GetDamageFormula();
}