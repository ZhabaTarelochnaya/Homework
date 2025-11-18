using ConsoleTBS.Potions;

namespace ConsoleTBS;

public class Player : ICharacter
{
    public CharacterName Name => CharacterName.Player;
    public int MaxHealth { get; }
    public int CurrentHealth { get; private set; }
    public int BaseDamage { get; }

    public Player(int maxHealth, int currentHealth, int baseDamage)
    {
        MaxHealth = maxHealth;
        CurrentHealth = currentHealth;
        BaseDamage = baseDamage;
    }
    public void Attack(ICharacter character) => character.Hurt(BaseDamage);
    public void Hurt(int damage) => CurrentHealth = Math.Max(CurrentHealth - damage, 0);
    public void Heal(int heal) => CurrentHealth = Math.Min(CurrentHealth + heal, 0);
    public void Consume(IConsumable consumable) => consumable.ConsumeBy(this);
}