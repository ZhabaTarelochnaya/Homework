using ConsoleTBS.Effects;
using ConsoleTBS.Potions;
using ConsoleTBS.Weapons;

namespace ConsoleTBS;

public class Enemy : IEnemy
{
    readonly Character _character;
    public CharacterName Name => CharacterName.Enemy;
    public int MaxHealth => _character.MaxHealth;
    public int CurrentHealth => _character.CurrentHealth;
    public int BaseDamage
    {
        get => _character.BaseDamage;
        set => _character.BaseDamage = value;
    }
    public IWeapon Weapon => _character.Weapon;
    public EffectProcessor EffectProcessor => _character.EffectProcessor;
    public IConsumable CurrentConsumable => _character.CurrentConsumable;
    public IEnumerable<IConsumable> Consumables => _character.Consumables;
    public int Reward { get; }
    public Enemy(Character character, int reward)
    {
        Reward = reward;
        _character = character;
    }
    public void EquipWeapon(IWeapon weapon) => _character.EquipWeapon(weapon);
    public void UnequipCurrentWeapon() => _character.UnequipCurrentWeapon();
    public int Attack(ICharacter character) => _character.Attack(character);
    public void Hurt(int damage) => _character.Hurt(damage);
    public void Heal(int heal) => _character.Heal(heal);
    public void Consume(IConsumable consumable) => _character.Consume(consumable);
    public void AddConsumable(IConsumable consumable) => _character.AddConsumable(consumable);
    public IConsumable SwitchCurrentConsumable(int index) => _character.SwitchCurrentConsumable(index);
    public bool TryConsumeCurrent(out IConsumable? consumable) => _character.TryConsumeCurrent(out consumable);
}