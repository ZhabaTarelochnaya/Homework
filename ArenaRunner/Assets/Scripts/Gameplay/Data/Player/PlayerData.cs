using System;
using UnityEngine;
using Utils;

namespace DefaultNamespace.Gameplay.Data
{
    [Serializable]
    public class PlayerData
    {
        public float Speed;
        public bool IsDead;
        public SerializableVector2 Position;
    }
}