using System;

namespace _Prototype.Scripts.Gameplay.View
{
    public interface ICollectorView
    {
        public event Action<int> PickUpCollected;
    }
}