using System;
using UnityEngine;

namespace Utils
{
    [Serializable]
    public struct SerializableVector2
    {
        public float x;
        public float y;
        
        public SerializableVector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }
        public SerializableVector2(Vector3 vector)
        {
            x = vector.x;
            y = vector.y;
        }

        public static implicit operator Vector2(SerializableVector2 sv)
        {
            return new Vector2(sv.x, sv.y);
        }
        public static implicit operator SerializableVector2(Vector2 v)
        {
            return new SerializableVector2(v.x, v.y);
        }
    }
}