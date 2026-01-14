using System;
using Unity.VisualScripting;
using UnityEngine;

namespace DefaultNamespace.Gameplay.View.PickUp
{
    public class PickUpCollectorView : MonoBehaviour
    {
        PickUpCollectorViewModel _viewModel;
        public void Bind(PickUpCollectorViewModel viewModel)
        {
            _viewModel = viewModel;
        }
        void OnTriggerEnter(Collider other)
        {
            var pickUp = other.GetComponent<PickUpView>();
            _viewModel.Collect(pickUp.ID);
        }
    }
}