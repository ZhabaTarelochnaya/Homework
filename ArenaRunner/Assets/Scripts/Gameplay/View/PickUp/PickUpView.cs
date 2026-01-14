using System;
using DefaultNamespace.Gameplay.Data.PickUp;
using UnityEngine;

namespace DefaultNamespace.Gameplay.View.PickUp
{
    public class PickUpView : MonoBehaviour
    {
        PickUpViewModel _pickUpViewModel;
        [field: SerializeField] public PickUpType PickUpType { get; private set; }
        public int ID { get => _pickUpViewModel.ID; }
        public void Bind(PickUpViewModel pickUpViewModel)
        {
            _pickUpViewModel = pickUpViewModel;
            PickUpType = pickUpViewModel.PickUpType;
            _pickUpViewModel.Collected += PickUpViewModelOnCollected;
        }

        void PickUpViewModelOnCollected()
        {
            Destroy(gameObject);
        }

        void OnDestroy()
        {
            if (_pickUpViewModel == null) return;
            _pickUpViewModel.Collected -= PickUpViewModelOnCollected;
        }
    }
}