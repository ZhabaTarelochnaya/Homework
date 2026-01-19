namespace Gameplay.View.World.Enemies.HitHurtBoxes
{
    public interface IHealthUser
    {
        public int MaxHealth { get; set; }
        public int CurrentHealth { get; set; }
    }
}