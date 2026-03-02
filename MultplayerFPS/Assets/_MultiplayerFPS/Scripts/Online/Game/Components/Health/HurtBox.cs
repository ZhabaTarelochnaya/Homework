using System;
using Mirror;
using UnityEngine;

namespace _MultiplayerFPS.Scripts.Components.Health
{
    public class HurtBox : MonoBehaviour
    {
        [SerializeField] Health _health;
        public Health Health => _health;
        [field: SerializeField] public int DamageMultiplayer { get; private set; } = 1;

        void OnTriggerEnter(Collider other)
        {
            var hitBox = other.GetComponent<HitBox>();
            _health.Damage(hitBox.Damage * DamageMultiplayer);
        }
        public void Damage(int damage)
        {
            _health.Damage(damage * DamageMultiplayer);
        }
    }
}