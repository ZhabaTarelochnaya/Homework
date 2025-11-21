using ConsoleTBS.Effects;
using ConsoleTBS.Potions;
using ConsoleTBS.Weapons;

namespace ConsoleTBS;

public class Character : ICharacter
{
    List<IConsumable> _consumables = new ();
    int _baseDamage;
    public EffectProcessor EffectProcessor { get; }
    public IConsumable? CurrentConsumable { get; private set; }
    public IEnumerable<IConsumable> Consumables => _consumables;
    public CharacterName Name => CharacterName.Default;
    public int MaxHealth { get; }
    public int CurrentHealth { get; private set; }
    public int BaseDamage
    {
        get => _baseDamage;
        set => _baseDamage = value < 0 ? 0 : value;
    }
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
        EffectProcessor = new EffectProcessor(this);
    }
    public int Attack(ICharacter character)
    {
        var damage = BaseDamage + (Weapon?.GetDamage() ?? 0);
        character.Hurt(damage);
        return damage;
    }

    public void Hurt(int damage) => CurrentHealth = Math.Max(CurrentHealth - damage, 0);
    public void Heal(int heal) => CurrentHealth = Math.Min(CurrentHealth + heal, MaxHealth);

    public void Consume(IConsumable consumable)
    {
        EffectProcessor.Add(consumable.Consume());
        _consumables.Remove(consumable);
    }
    public void AddConsumable(IConsumable consumable)
    {
        _consumables.Add(consumable);
        CurrentConsumable ??= consumable;
    }
    public IConsumable SwitchCurrentConsumable(int index) => CurrentConsumable = _consumables[index];

    public bool TryConsumeCurrent(out IConsumable? consumable)
    {
        if (CurrentConsumable == null)
        {
            consumable = null;
            return false;
        }
        consumable = CurrentConsumable;
        Consume(consumable);
        CurrentConsumable = null;
        return true;
    }
}