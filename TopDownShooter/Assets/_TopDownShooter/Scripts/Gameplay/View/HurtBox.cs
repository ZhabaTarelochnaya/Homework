using System;
using UnityEngine;

namespace _TopDownShooter.Scripts.View
{
    public class HurtBox : MonoBehaviour
    {
        [field: SerializeField] public int MaxHealth { get; set; }
        public int CurrentHealth { get; private set; }
        
        public delegate void HitHandler(int damage, int currentHealth);
        public event HitHandler Hit;
        public event Action Died;
        void Awake()
        {
            CurrentHealth = MaxHealth;
        }
        void OnTriggerEnter(Collider other)
        {
            var hitBox = other.GetComponent<HitBox>();
            if (!hitBox) return;
            TakeDamage(hitBox.Damage);
        }
        public void HealFullHealth() => CurrentHealth = MaxHealth;
        public void TakeDamage(int damage)
        {
            CurrentHealth -= damage;
            Hit?.Invoke(damage, CurrentHealth);
            if (CurrentHealth <= 0)
            {
                CurrentHealth = 0;
                Died?.Invoke();
            }
            else if (CurrentHealth > MaxHealth)
            {
                CurrentHealth = MaxHealth;
            }
        }
    }
}