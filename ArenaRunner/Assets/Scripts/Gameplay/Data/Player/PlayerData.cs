using System;
using UnityEngine;
using Utils;

namespace DefaultNamespace.Gameplay.Data
{
    [Serializable]
    public class PlayerData
    {
        public float Speed;
        public SerializableVector2 Position;
        public int CurrentHealth;
        public int MaxHealth;
    }
}