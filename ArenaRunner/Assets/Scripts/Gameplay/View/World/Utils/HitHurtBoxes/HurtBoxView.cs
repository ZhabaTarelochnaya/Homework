using System;
using UnityEngine;

namespace Gameplay.View.World.Enemies.HitHurtBoxes
{
    public class HurtBoxView : MonoBehaviour
    {
        HurtBoxViewModel _hurtBoxViewModel;

        public void Bind(HurtBoxViewModel hurtBoxViewModel)
        {
            _hurtBoxViewModel = hurtBoxViewModel;
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            var hitBoxView = other.GetComponent<HitBoxView>();
            _hurtBoxViewModel.DealDamage(hitBoxView.Damage);
        }
    }
}