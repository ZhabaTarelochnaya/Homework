using System;
using DefaultNamespace.Gameplay.World;
using UnityEngine;

namespace DefaultNamespace.Gameplay.Data.PickUp
{
    [Serializable]
    public class PickUpData
    {
        public PickUpType Type;
        public int Price;
        public Vector3 Position;
        public int ID;
    }
}