namespace ConsoleTBS.Weapons;

public class Sword : Item, IWeapon
{
    const int DamageRange = 6;
    readonly Random rng = new ();
    readonly int _baseDamage;
    public override ItemName Name => ItemName.Sword;
    public Sword(int baseDamage, int price) : base(price)
    {
        _baseDamage = baseDamage;
    }
    public int GetDamage() => _baseDamage + rng.Next(0, DamageRange);
    public string GetDamageFormula() => $"{_baseDamage - 1} + {1}-{DamageRange}";
    public override void Use(ICharacter character) => character.EquipWeapon(this);
}