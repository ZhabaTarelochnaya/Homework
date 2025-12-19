using System;
using UnityEngine;

namespace NPCEventNotification.Scripts.Gameplay.NPC
{
    public class Health
    {
        readonly IHealthData _healthData;
        public int MaxHealth => _healthData.MaxHealth;
        public int CurrentHealth => _healthData.CurrentHealth;
        public event Action<int, Health> HealthLost;
        public event Action<int, Health> HealthGained;
        public event Action<int, Health> HealthChanged;
        public event Action Died;

        public Health(IHealthData healthData)
        {
            _healthData = healthData;
        }

        public void Hurt(int damage)
        {
            if (damage < 1)
            {
                Debug.LogWarning("Damage is less than 1");
                return;
            }
            _healthData.CurrentHealth -= damage;
            _healthData.CurrentHealth = CurrentHealth < 0 ? 0 : CurrentHealth;
            HealthChanged?.Invoke(-damage, this);
            HealthLost?.Invoke(damage, this);
            if (_healthData.CurrentHealth <= 0)
            {
                Died?.Invoke();
            }
        }

        public void Heal(int heal)
        {
            if (heal < 1)
            {
                Debug.LogWarning("Heal is less than 1");
                return;
            }
            _healthData.CurrentHealth += heal;
            _healthData.CurrentHealth = CurrentHealth > MaxHealth ? MaxHealth : CurrentHealth;
            HealthChanged?.Invoke(heal, this);
            HealthGained?.Invoke(heal, this);
        }
    }
}