using _Prototype.Scripts.Gameplay.View.HurtBox;
using UnityEngine;

namespace _Prototype.Scripts.Gameplay.View
{
    public class DamageAreaView : MonoBehaviour, IDamageAreaView
    {
        [SerializeField] HitBoxView _hitBoxView;
        public int ID { get; set; }
        public IHitBoxView HitBoxView => _hitBoxView;
    }
}