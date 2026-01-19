namespace Gameplay.View.World.Enemies.HitHurtBoxes
{
    public class HitBoxViewModel
    {
        readonly IDamageDealer _damageDealer;
        public int Damage => _damageDealer.Damage;

        public HitBoxViewModel(IDamageDealer damageDealer)
        {
            _damageDealer = damageDealer;
        }
    }
}