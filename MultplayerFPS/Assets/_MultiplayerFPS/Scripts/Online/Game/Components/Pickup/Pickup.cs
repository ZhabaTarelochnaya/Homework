using _MultiplayerFPS.Scripts.Config.Pickup;
using _MultiplayerFPS.Scripts.Services;
using _MultiplayerFPS.Scripts.Utils.ServiceLocator;
using Mirror;
using UnityEngine;

namespace _MultiplayerFPS.Scripts.Components
{
    public class Pickup : NetworkBehaviour
    {
        IPickupService _pickupService;
        [SyncVar, HideInInspector]
        public bool IsPickedUp;
        [field: SerializeField] public PickupName Name { get; private set; }
        public override void OnStartServer()
        {
            _pickupService = ServiceLocator.Current.Get<PickupService>();
        }

        [Command]
        public void CmdTryPickUp()
        {
            _pickupService.TryPickup(netId, connectionToClient.identity.netId);
        }

        [ClientRpc]
        public void RpcSetActive(bool isActive) => gameObject.SetActive(isActive);
    }
}