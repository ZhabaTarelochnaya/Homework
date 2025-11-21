namespace ConsoleTBS.Weapons.Types;

public class Daggers : Item, IWeapon
{
    const int DamageRange = 4;
    readonly Random rng = new ();
    readonly int _baseDamage;
    public override ItemName Name => ItemName.Daggers;
    
    public Daggers(int baseDamage, int price) : base(price)
    {
        _baseDamage = baseDamage;
    }
    public int GetDamage() => _baseDamage + rng.Next(0, DamageRange) + rng.Next(0, DamageRange);
    public string GetDamageFormula() => $"{_baseDamage - 1} + {1}-{DamageRange} + {1}-{DamageRange}";
    public override void Use(ICharacter character) => character.EquipWeapon(this);
}