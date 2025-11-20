using ConsoleTBS.Potions;
using ConsoleTBS.Weapons;

namespace ConsoleTBS;

public interface ICharacter : IWeaponWielder, IConsumer
{
    public CharacterName Name { get; }
    public int MaxHealth { get; }
    public int CurrentHealth { get; }
    public int BaseDamage { get; set; }
    public void Hurt(int damage);
    public void Heal(int heal);
    public int Attack(ICharacter character);
}