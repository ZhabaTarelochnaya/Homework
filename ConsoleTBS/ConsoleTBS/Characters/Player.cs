using ConsoleTBS.Effects;
using ConsoleTBS.Potions;
using ConsoleTBS.Weapons;

namespace ConsoleTBS;

public class Player : ICharacter
{
    readonly Character _character;
    int _coinsLeft;
    List<IWeapon> _weapons = new();
    public CharacterName Name => CharacterName.Player;
    public int MaxHealth => _character.MaxHealth;
    public int CurrentHealth => _character.CurrentHealth;
    public int BaseDamage
    {
        get => _character.BaseDamage;
        set => _character.BaseDamage = value;
    }
    public IWeapon? Weapon => _character.Weapon;
    public IEnumerable<IWeapon> Weapons => _weapons;
    public EffectProcessor EffectProcessor => _character.EffectProcessor;
    public IConsumable CurrentConsumable => _character.CurrentConsumable;
    public IEnumerable<IConsumable> Consumables => _character.Consumables;
    public int CoinsLeft
    {
        get => _coinsLeft;
        set => _coinsLeft = value < 0 ? 0 : value;
    }
    public Player(Character character)
    {
        _character = character;
    }

    public void EquipWeapon(IWeapon weapon)
    {
        if (!_weapons.Contains(weapon))
        { 
            _weapons.Add(weapon);
        }
        _character.EquipWeapon(weapon);
    }

    public void UnequipCurrentWeapon() => _character.UnequipCurrentWeapon();
    public void AddWeapon(IWeapon weapon)
    {
        _weapons.Add(weapon);
        if (Weapon == null)
        {
            EquipWeapon(weapon);
        }
    }
    public IWeapon SwitchCurrentWeapon(int index)
    {
        EquipWeapon(_weapons[index]);
        return _weapons[index];
    }

    public int Attack(ICharacter character) => _character.Attack(character);
    public void Hurt(int damage) => _character.Hurt(damage);
    public void Heal(int heal) => _character.Heal(heal);
    public void Consume(IConsumable consumable) => _character.Consume(consumable);
    public void AddConsumable(IConsumable consumable) => _character.AddConsumable(consumable);
    public IConsumable SwitchCurrentConsumable(int index) => _character.SwitchCurrentConsumable(index);
    public bool TryConsumeCurrent(out IConsumable? consumable) => _character.TryConsumeCurrent(out consumable);
}