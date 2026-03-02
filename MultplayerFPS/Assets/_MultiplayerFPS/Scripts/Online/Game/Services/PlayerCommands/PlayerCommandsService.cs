using System;

namespace _MultiplayerFPS.Scripts.Services.ServerCommands
{
    public class PlayerCommandsService : IPlayerCommandsService
    {
        readonly GameNetworkPlayer _gameNetworkPlayer;

        public PlayerCommandsService(GameNetworkPlayer gameNetworkPlayer)
        {
            _gameNetworkPlayer = gameNetworkPlayer;
        }
        public void ReturnToLobby() => _gameNetworkPlayer.CmdReturnToLobby();
    }
}