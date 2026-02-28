using _MultiplayerFPS.Scripts.Components;
using UnityEngine;

namespace _MultiplayerFPS.Scripts.Config.Pickup
{
    public abstract class PickupConfig : ScriptableObject
    {
        public abstract PickupName Name { get; protected set; }
        public abstract Components.Pickup Prefab { get; protected set; }
    }
}