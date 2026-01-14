using System;
using DefaultNamespace.Gameplay.Data.PickUp;
using UnityEngine;

namespace DefaultNamespace.Gameplay.View.PickUp
{
    public class PickUpViewModel
    {
        readonly PickUpState _pickUpState;
        public int ID { get => _pickUpState.ID; }
        public Vector3 Position { get => _pickUpState.Position; }
        public PickUpType PickUpType { get => _pickUpState.Type; }
        public event Action Collected;

        public PickUpViewModel(PickUpState pickUpState)
        {
            _pickUpState = pickUpState;
            _pickUpState.Collected += () => Collected?.Invoke();
        }
    }
}