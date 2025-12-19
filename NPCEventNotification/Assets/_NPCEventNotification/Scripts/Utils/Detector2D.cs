using System;
using UnityEngine;

namespace NPCEventNotification.Scripts.Utils
{
    public class Detector2D : MonoBehaviour
    {
        public event Action<Collider2D> TriggerEntered;
        public event Action<Collider2D> TriggerExited;
        public event Action<Collider2D> TriggerStay;

        void Awake()
        {
            if (!GetComponent<Collider2D>()) Debug.LogError($"{name} does not have a Collider2D attached.");
        }

        void OnTriggerEnter2D(Collider2D other) => TriggerEntered?.Invoke(other);
        void OnTriggerExit2D(Collider2D other) => TriggerExited?.Invoke(other);
        void OnTriggerStay2D(Collider2D other) => TriggerStay?.Invoke(other); 
    }
}