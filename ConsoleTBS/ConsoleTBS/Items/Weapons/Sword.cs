namespace ConsoleTBS.Weapons;

public class Sword : Item, IWeapon
{
    readonly Random rng = new ();
    readonly int BaseDamage = 4;
    readonly int DamageRange = 3;
    
    public override ItemName Name { get; }
    
    public Sword() : base(20) { }
    
    public override void Use(ICharacter character) => character.EquipWeapon(this);
    public int GetDamage() => BaseDamage + rng.Next(0, DamageRange);
}