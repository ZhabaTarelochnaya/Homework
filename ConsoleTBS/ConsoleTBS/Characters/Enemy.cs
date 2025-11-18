using ConsoleTBS.Potions;
using ConsoleTBS.Weapons;

namespace ConsoleTBS;

public class Enemy : ICharacter
{
    readonly Character _character;
    public CharacterName Name => CharacterName.Player;
    public int MaxHealth => _character.MaxHealth;
    public int CurrentHealth => _character.CurrentHealth;
    public int BaseDamage => _character.BaseDamage;
    public IWeapon Weapon => _character.Weapon;
    public Enemy(Character character)
    {
        _character = character;
    }
    public void EquipWeapon(IWeapon weapon) => _character.EquipWeapon(weapon);
    public void UnequipCurrentWeapon() => _character.UnequipCurrentWeapon();
    public void Attack(ICharacter character) => _character.Attack(character);
    public void Hurt(int damage) => _character.Hurt(damage);
    public void Heal(int heal) => _character.Heal(heal);
    public void Consume(IConsumable consumable) => _character.Consume(consumable);
}