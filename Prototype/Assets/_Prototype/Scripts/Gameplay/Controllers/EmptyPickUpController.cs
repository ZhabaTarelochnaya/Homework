using _Prototype.Scripts.Gameplay.Services.PickUpService;
using _Prototype.Scripts.Gameplay.View;
using UnityEngine;

namespace _Prototype.Scripts.Gameplay.Controllers
{
    public class EmptyPickUpController : PickUp
    {
        readonly IPickUpView _pickUpView;
        public EmptyPickUpController(IPickUpView pickUpView) : base(pickUpView)
        {
            _pickUpView = pickUpView;
        }
        public override void Collect()
        {
            _pickUpView.Collect();
        }
    }
}