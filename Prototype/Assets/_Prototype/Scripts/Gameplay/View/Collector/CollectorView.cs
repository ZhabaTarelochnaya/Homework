using System;
using _Prototype.Scripts.Gameplay.Services.PickUpService;
using UnityEngine;

namespace _Prototype.Scripts.Gameplay.View
{
    public class CollectorView :  MonoBehaviour, ICollectorView
    {
        public event Action<int> PickUpCollected;
        void OnTriggerEnter2D(Collider2D other)
        {
            var pickUp = other.GetComponent<IPickUpView>();
            PickUpCollected?.Invoke(pickUp.ID);
        }
    }
}