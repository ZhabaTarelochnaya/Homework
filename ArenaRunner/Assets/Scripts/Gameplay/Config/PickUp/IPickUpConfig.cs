using DefaultNamespace.Gameplay.Data.PickUp;

namespace DefaultNamespace.Gameplay.Data
{
    public interface IPickUpConfig
    {
        public PickUpType Type { get; }
        public int Price { get; }

        public PickUpData Create();
    }
}