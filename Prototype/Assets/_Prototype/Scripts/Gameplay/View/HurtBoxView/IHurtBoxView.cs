using System;

namespace _Prototype.Scripts.Gameplay.View.HurtBox
{
    public interface IHurtBoxView
    {
        public event Action<int> Hurt;
    }
}