using System;
using UnityEngine;

namespace _MultiplayerFPS.Scripts.Services.ServerCommands
{
    public class PlayerCommandsService : IPlayerCommandsService
    {
        readonly GameNetworkPlayerCommands _gameNetworkPlayerCommands;

        public PlayerCommandsService(GameNetworkPlayerCommands gameNetworkPlayerCommands)
        {
            _gameNetworkPlayerCommands = gameNetworkPlayerCommands;
        }
        public void CmdReturnToLobby() => _gameNetworkPlayerCommands.CmdReturnToLobby(); 
        public void CmdTryPickUp(uint netId) => _gameNetworkPlayerCommands.CmdTryPickUp(netId);
        public void CmdThrowGrenade(Vector3 direction) => _gameNetworkPlayerCommands.CmdThrowGrenade(direction);
        public void CmdUseMedKit() => _gameNetworkPlayerCommands.CmdUseMedKit();
        public void CmdChangeInitialized(bool newValue) => _gameNetworkPlayerCommands.CmdChangeInitialized(newValue);
        public void CmdChangeIsHealing(bool value) => _gameNetworkPlayerCommands.CmdChangeIsHealing(value);
        public void CmdChangeIsThrowingGrenade(bool value) => _gameNetworkPlayerCommands.CmdChangeIsThrowingGrenade(value);
    }
}