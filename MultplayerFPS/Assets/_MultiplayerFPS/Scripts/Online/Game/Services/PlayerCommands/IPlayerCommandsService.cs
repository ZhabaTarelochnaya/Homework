using _MultiplayerFPS.Scripts.Utils.ServiceLocator;
using UnityEngine;

namespace _MultiplayerFPS.Scripts.Services.ServerCommands
{
    public interface IPlayerCommandsService : IService
    {
        public void CmdReturnToLobby();
        public void CmdTryPickUp(uint netId);
        public void CmdThrowGrenade(Vector3 direction);
        public void CmdUseMedKit();
        public void CmdChangeInitialized(bool newValue);
        public void CmdChangeIsHealing(bool value);
        public void CmdChangeIsThrowingGrenade(bool value);
    }
}