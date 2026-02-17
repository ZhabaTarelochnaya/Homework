using System;
using UnityEngine;

namespace _Prototype.Scripts.Gameplay.View.HurtBox
{
    public class HurtBoxView : MonoBehaviour, IHurtBoxView
    {
        public event Action<int> Hurt;
        void OnTriggerEnter2D(Collider2D other)
        {
            var hitbox = other.GetComponent<IHitBoxView>();
            Hurt?.Invoke(hitbox.Damage);
        }
    }
}