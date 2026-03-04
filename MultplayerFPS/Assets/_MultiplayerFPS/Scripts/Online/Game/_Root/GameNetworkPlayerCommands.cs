using _MultiplayerFPS.Scripts.Components.Health;
using _MultiplayerFPS.Scripts.Config.Pickup;
using _MultiplayerFPS.Scripts.Services;
using _MultiplayerFPS.Scripts.Services.Config;
using _MultiplayerFPS.Scripts.Services.GrenadeService;
using _MultiplayerFPS.Scripts.Services.State;
using _MultiplayerFPS.Scripts.State;
using _MultiplayerFPS.Scripts.Utils.ServiceLocator;
using Mirror;
using UnityEngine;

namespace _MultiplayerFPS.Scripts
{
    public class GameNetworkPlayerCommands : NetworkBehaviour
    {
        [SerializeField] PlayerState _playerState;
        IPickupService _pickupService;
        IGrenadeService _grenadeService;
        IStateService _stateService;
        IPlayerScoreService _playerScoreService;
        MedKitConfig _medKitConfig;

        public override void OnStartServer()
        {
            _pickupService = ServiceLocator.Current.Get<IPickupService>();
            _grenadeService = ServiceLocator.Current.Get<IGrenadeService>();
            _stateService = ServiceLocator.Current.Get<IStateService>();
            _playerScoreService = ServiceLocator.Current.Get<IPlayerScoreService>();
            _medKitConfig = ServiceLocator.Current.Get<IConfigService>().Get<MedKitConfig>();
        }
        [Command]
        public void CmdTryPickUp(uint netId)
        {
            _pickupService.TryPickup(netId, connectionToClient.identity.netId);
        }
        [Command]
        public void CmdThrowGrenade(Vector3 direction)
        {
            _grenadeService.ThrowGrenade(_playerState, transform.position, direction);
        }
        [Command]
        public void CmdReturnToLobby()
        {
            _stateService.GameState.GameStateName = GameStateName.Unregister;
            NetManager.singleton.ServerChangeScene("Lobby");
        }
        [Command]
        public void CmdUseMedKit()
        {
            var isRemoved =  _playerState.Pickups.Remove(PickupName.MedKit);
            if (!isRemoved) return;
            _playerState.Damage(-_medKitConfig.Heal);
        }

        [Command]
        public void CmdAddDeath()
        {
            if (!_playerState.IsDead) return;
            _playerScoreService.AddDeath(netId);
        }
        [Command]
        public void CmdChangeInitialized(bool newValue) => _playerState.IsInitialized = newValue;
        [Command]
        public void CmdChangeIsHealing(bool value) => _playerState.IsHealing = value;
        [Command]
        public void CmdChangeIsThrowingGrenade(bool value) => _playerState.IsThrowingGrenade = value;
    }
}