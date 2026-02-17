using UnityEngine;

namespace _Prototype.Scripts.Gameplay.View.HurtBox
{
    public class HitBoxView : MonoBehaviour, IHitBoxView
    {
        public int Damage { get; set; }
    }
}