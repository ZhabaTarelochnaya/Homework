using System;
using _MultiplayerFPS.Scripts.Services.InputService;
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
            RegisterLocalServices();
        }
        public override void OnStartServer()
        {
            RegisterServerServices();
        }
        public override void OnStopClient()
        {
            UnregisterLocalServices();
        }
        public override void OnStopServer()
        {
            UnregisterServerServices();
        }
        void RegisterLocalServices()
        {
            var inputService = new MouseKeyboardInputService();
            ServiceLocator.Current.Register<IInputService>(inputService);
        }
        void UnregisterLocalServices()
        {
            ServiceLocator.Current.Unregister<IInputService>();
        }

        void RegisterServerServices()
        {
            
        }
        void UnregisterServerServices()
        {
            
        }
        
    }
}