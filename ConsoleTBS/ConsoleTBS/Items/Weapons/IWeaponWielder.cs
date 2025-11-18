namespace ConsoleTBS.Weapons;

public interface IWeaponWielder
{
    public IWeapon Weapon { get; }
    public void EquipWeapon(IWeapon weapon);
    public void UnequipCurrentWeapon();
    public void Attack(ICharacter character);
}