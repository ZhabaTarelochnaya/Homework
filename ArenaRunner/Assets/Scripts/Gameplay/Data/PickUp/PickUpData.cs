using System;
using DefaultNamespace.Gameplay.World;
using UnityEngine;
using Utils;

namespace DefaultNamespace.Gameplay.Data.PickUp
{
    [Serializable]
    public class PickUpData
    {
        public PickUpType Type;
        public int Score;
        public SerializableVector2 Position;
        public int ID;
    }
}