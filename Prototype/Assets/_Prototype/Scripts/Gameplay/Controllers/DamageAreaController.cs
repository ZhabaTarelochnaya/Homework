using _Prototype.Scripts.Gameplay.Controllers.HitBoxController;
using _Prototype.Scripts.Gameplay.Services.GameDataService;
using _Prototype.Scripts.Gameplay.View;
using _Prototype.Scripts.Gameplay.View.HurtBox;
using _Prototype.Scripts.Utils.ServiceLocator;

namespace _Prototype.Scripts.Gameplay.Controllers
{
    public class DamageAreaController : IHitBoxController
    {
        public int Damage { get; set; }
        public int ID { get; }
        public DamageAreaController(IDamageAreaView damageAreaView)
        {
            damageAreaView.HitBoxView.Damage = Damage;
            var gameStateService = ServiceLocator.Current.Get<GameStateService>();
            ID = gameStateService.CreateID();
            damageAreaView.ID = ID;
        }
    }
}