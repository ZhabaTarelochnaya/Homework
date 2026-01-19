using UnityEngine;

namespace Gameplay.View.World.Enemies.HitHurtBoxes
{
    public class HitBoxView : MonoBehaviour
    {
        HitBoxViewModel _hitBoxViewModel;
        public int Damage => _hitBoxViewModel.Damage;
        public void Bind(HitBoxViewModel hitBoxViewModel)
        {
            _hitBoxViewModel = hitBoxViewModel;
        }
    }
}