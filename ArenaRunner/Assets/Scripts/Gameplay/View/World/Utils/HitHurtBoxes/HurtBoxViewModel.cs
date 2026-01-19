using Gameplay.Services;
using Utils.EventBus;
using Utils.ServiceLocator;

namespace Gameplay.View.World.Enemies.HitHurtBoxes
{
    public class HurtBoxViewModel
    {
        readonly EventBus _eventBus;
        readonly IHealthUser _healthUser;
        readonly HealthService _healthService;

        public HurtBoxViewModel(IHealthUser healthUser)
        {
            _healthUser = healthUser;
            _eventBus = ServiceLocator.Current.Get<EventBus>();
            _healthService = ServiceLocator.Current.Get<HealthService>();
        }
        public void DealDamage(int damage)
        {
            _healthService.DealDamage(_healthUser, damage);
        }
    }
}