using ConsoleTBS.Potions;
using ConsoleTBS.Weapons;

namespace ConsoleTBS;

public class Character : ICharacter
{
    public CharacterName Name => CharacterName.Default;
    public int MaxHealth { get; }
    public int CurrentHealth { get; private set; }
    public int BaseDamage { get; }
    public IWeapon Weapon { get; private set; }
    public void EquipWeapon(IWeapon weapon)
    {
        UnequipCurrentWeapon();
        Weapon = weapon;
    }
    public void UnequipCurrentWeapon() => Weapon = null;

    public Character(int maxHealth, int baseDamage)
    {
        MaxHealth = maxHealth;
        CurrentHealth = maxHealth;
        BaseDamage = baseDamage;
    }
    public void Attack(ICharacter character)
    {
        var damage = BaseDamage + (Weapon?.GetDamage() ?? 0);
        character.Hurt(damage);
    }

    public void Hurt(int damage) => CurrentHealth = Math.Max(CurrentHealth - damage, 0);
    public void Heal(int heal) => CurrentHealth = Math.Min(CurrentHealth + heal, 0);
    public void Consume(IConsumable consumable) => consumable.ConsumeBy(this);
}