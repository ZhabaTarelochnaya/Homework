using System;
using _MultiplayerFPS.Scripts.Controllers;
using _MultiplayerFPS.Scripts.Services.InputService;
using _MultiplayerFPS.Scripts.Services.Move;
using _MultiplayerFPS.Scripts.Utils.ServiceLocator;
using Mirror;
using UnityEngine;

namespace _MultiplayerFPS.Scripts
{
    [DefaultExecutionOrder(-1000)]
    public class GameRoot : NetworkBehaviour
    {
        public override void OnStartClient()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        public override void OnStartServer()
        {
        }
        public override void OnStopServer()
        {
            
        }
        public override void OnStopClient()
        {
        }
    }
}