using System;
using _MultiplayerFPS.Scripts.Config;
using _MultiplayerFPS.Scripts.Services.Config;
using _MultiplayerFPS.Scripts.State;
using _MultiplayerFPS.Scripts.Utils.ServiceLocator;
using Mirror;
using UnityEngine;

namespace _MultiplayerFPS.Scripts.Components
{
    [RequireComponent(typeof(SphereCollider))]
    public class PickupCollector : NetworkBehaviour
    {
        PlayerState _playerState;
        SphereCollider _collider;
        public override void OnStartClient()
        {
            Destroy(this);
        }
        public override void OnStartServer()
        {
            _collider = GetComponent<SphereCollider>();
            _collider.radius = ServiceLocator.Current.Get<IConfigService>()
                .Get<PlayerConfig>()
                .PickupRange;
        }
        void OnTriggerEnter(Collider other)
        {
            if (!isLocalPlayer) return;
            
            var pickUp = other.GetComponent<Pickup>();
            if (pickUp)
            {
                pickUp.CmdTryPickUp();
            }
        }
    }
}