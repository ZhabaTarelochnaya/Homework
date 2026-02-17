namespace _Prototype.Scripts.Gameplay.Controllers.HitBoxController
{
    public interface IHurtBoxController
    {
        public int CurrentHealth { get; set; }
        public int MaxHealth { get; set; }
    }
}