using Gameplay.View.World.Enemies.HitHurtBoxes;
using Utils.ServiceLocator;

namespace Gameplay.Services
{
    public class HealthService : IService
    {
        public void DealDamage(IHealthUser healthUser, int damage)
        {
            healthUser.CurrentHealth -= damage;
            if (healthUser.CurrentHealth < 0)
            {
                healthUser.CurrentHealth = 0;
            }
            else if (healthUser.CurrentHealth > healthUser.MaxHealth)
            {
                healthUser.CurrentHealth = healthUser.MaxHealth;
            }
        }
    }
}