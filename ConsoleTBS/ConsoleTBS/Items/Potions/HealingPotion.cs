namespace ConsoleTBS.Potions;

public class HealingPotion : Item, IConsumable
{
    readonly int HealingAmount = 10;
    
    public override ItemName Name => ItemName.HealingPotion;
    
    public HealingPotion() : base(10) { }
    public override void Use(ICharacter character) => character.Heal(HealingAmount);
    public void ConsumeBy(ICharacter character) => Use(character);
}