using ConsoleTBS.Weapons;

namespace ConsoleTBS;

public class WeaponsRenderer
{
    public void ShowCurrentWeapon(IWeaponWielder weaponWielder)
    {
        string equippedWeapon;
        if (weaponWielder.Weapon != null)
        {
            Console.Write("Equipped Weapon: ");
            ShowWeaponInfo(weaponWielder.Weapon);
        }
        else
        {
            Console.WriteLine($"Equipped Weapon: none");
        }
    }
    public void ShowAvailableWeapons(IEnumerable<IWeapon> weapons)
    {
        if (!weapons.Any())
        {
            Console.WriteLine("No weapons left");
            return;
        }
        Console.WriteLine($"Available weapons:");
        int i = 1;
        foreach (var weapon in weapons)
        {
            Console.Write($"{i} - ");
            ShowWeaponInfo(weapon);
            i++;
        }
    }
    public void ShowWeaponInfo(IWeapon weapon)
    {
        Console.WriteLine($"{weapon.Name}: {weapon.GetDamageFormula()} damage.");
    }
}