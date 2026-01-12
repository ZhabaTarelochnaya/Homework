using System;
using UnityEngine;

namespace DefaultNamespace.Gameplay.Data
{
    [Serializable]
    public class PlayerData
    {
        public float Speed;
        public bool IsDead;
        public Vector3 Position;
        public Vector3 CameraRotation;
        public float MouseSensitivity;
    }
}