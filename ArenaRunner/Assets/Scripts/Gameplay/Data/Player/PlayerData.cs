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
        public SerializableVector3 Position;
        public SerializableVector3 CameraRotation;
        public float MouseSensitivity;
    }
}