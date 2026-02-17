using _Prototype.Scripts.Gameplay.Controllers.HitBoxController;
using _Prototype.Scripts.Utils.ServiceLocator;

namespace _Prototype.Scripts.Gameplay.Services.HealthService
{
    public interface IHealthService : IService
    {
        public void Damage(IHurtBoxController hurtBox, int damage);
    }
}