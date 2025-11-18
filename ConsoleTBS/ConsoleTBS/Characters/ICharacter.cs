namespace ConsoleTBS;

public interface ICharacter
{
    public CharacterName Name { get; }
    public int MaxHealth { get; }
    public int CurrentHealth { get; }
    public int BaseDamage { get; }
    public void Attack(ICharacter character);
    public void Hurt(int damage);
    public void Heal(int heal);
}