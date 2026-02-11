using _Prototype.Scripts.Gameplay.Services.PickUpService;

namespace _Prototype.Scripts.Gameplay.View
{
    public interface IPickUpView
    {
        public void Collect();
        int ID { get; set; }
    }
}