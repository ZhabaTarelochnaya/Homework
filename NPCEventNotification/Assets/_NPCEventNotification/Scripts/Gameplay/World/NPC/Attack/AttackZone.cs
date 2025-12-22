using System;
using System.Collections.Generic;
using UnityEngine;

namespace NPCEventNotification.Scripts.Gameplay.NPC
{
    public class AttackZone : MonoBehaviour
    {
        List<Health> _targets = new ();
        Collider2D _collider;
        public event Action<Health, IEnumerable<Health>> TargetEntered;
        public event Action<Health, IEnumerable<Health>> TargetExited;

        void Awake()
        {
            _collider = GetComponent<Collider2D>();
            if (!_collider) Debug.LogError($"{gameObject.name} does not have a collider");
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            var healthUser = other.GetComponent<IHealthUser>();
            if (healthUser == null)
            {
                Debug.LogWarning(other.gameObject.name + " is not IHealthUser");
                return;
            }
            _targets.Add(healthUser.Health);
            TargetEntered?.Invoke(healthUser.Health, _targets);
        }
        void OnTriggerExit2D(Collider2D other)
        {
            var healthUser = other.GetComponent<IHealthUser>();
            if (healthUser == null)
            {
                Debug.LogWarning(other.gameObject.name + " is not IHealthUser");
                return;
            }
            _targets.Remove(healthUser.Health);
            TargetExited?.Invoke(healthUser.Health, _targets);
        }

        // public Health GetClosestTarget()
        // {
        //     _collider.Cast(Vector2.zero, _collider.);
        // }

        public bool TryAttack(int damage)
        {
            if (_targets.Count == 0)
            {
                return false;
            }
            _targets[0].Hurt(damage);
            return true;
        }
    }
}