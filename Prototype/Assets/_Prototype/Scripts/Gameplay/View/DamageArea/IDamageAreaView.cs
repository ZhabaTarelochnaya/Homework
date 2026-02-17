using _Prototype.Scripts.Gameplay.View.HurtBox;

namespace _Prototype.Scripts.Gameplay.View
{
    public interface IDamageAreaView
    {
        public int ID { get; set; }
        public IHitBoxView HitBoxView { get; }
    }
}