using System.Collections.Generic;
using _Prototype.Scripts.Gameplay.Controllers;
using _Prototype.Scripts.Gameplay.Services.PickUpService;

namespace _Prototype.Scripts.Gameplay.State
{
    public class GameState
    {
        public int ID { get; private set; }
        public List<PickUp> PickUps { get; } = new();
        public List<DamageAreaController> DamageAreas { get; }

        public int CreateID() => ID++;
    }
}