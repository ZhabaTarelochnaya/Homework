using System;
using _MultiplayerFPS.Scripts.State;
using Mirror;
using UnityEngine;

namespace _MultiplayerFPS.Scripts.Components
{
    [RequireComponent(typeof(SphereCollider))]
    public class PickupCollector : NetworkBehaviour
    {
        PlayerState _playerState;
        SphereCollider _collider;
        public event Action<uint> PickupCollected;
        void OnTriggerEnter(Collider other)
        {
            if (!isLocalPlayer) return;
            
            var pickUp = other.GetComponent<Pickup>();
            if (!pickUp) return;
            
            PickupCollected?.Invoke(pickUp.netId);
        }
    }
}