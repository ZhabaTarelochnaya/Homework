using _Prototype.Scripts.Gameplay.Controllers.HitBoxController;

namespace _Prototype.Scripts.Gameplay.Services.HealthService
{
    public class HealthService
    {
        public void Damage(IHurtBoxController hurtBox, int damage)
        {
            hurtBox.CurrentHealth -= damage;
            hurtBox.CurrentHealth = hurtBox.CurrentHealth < 0 ? 0 : hurtBox.CurrentHealth;
            hurtBox.CurrentHealth = hurtBox.CurrentHealth > hurtBox.MaxHealth ? hurtBox.MaxHealth : 0;
        }
    }
}