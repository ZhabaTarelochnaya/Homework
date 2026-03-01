using _MultiplayerFPS.Scripts.Config.Pickup;
using _MultiplayerFPS.Scripts.Services;
using _MultiplayerFPS.Scripts.Utils.ServiceLocator;
using Mirror;
using UnityEngine;

namespace _MultiplayerFPS.Scripts.Components
{
    public class Pickup : NetworkBehaviour
    {
        [SyncVar, HideInInspector]
        public bool IsPickedUp;
        [field: SerializeField] public PickupName Name { get; private set; }
        public Transform SpawnPoint { get; set; }
        [ClientRpc]
        public void RpcSetActive(bool isActive) => gameObject.SetActive(isActive);
    }
}