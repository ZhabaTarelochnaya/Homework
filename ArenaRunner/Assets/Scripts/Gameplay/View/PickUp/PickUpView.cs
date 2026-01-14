using UnityEngine;

namespace DefaultNamespace.Gameplay.View.PickUp
{
    public class PickUpView : MonoBehaviour
    {
        PickUpViewModel _pickUpViewModel;
        public int ID { get => _pickUpViewModel.ID; }
        public void Bind(PickUpViewModel pickUpViewModel)
        {
            _pickUpViewModel = pickUpViewModel;
        }
    }
}