using System;
using _MultiplayerFPS.Scripts.Services.InputService;
using _MultiplayerFPS.Scripts.Utils.ServiceLocator;
using UnityEngine;

namespace _MultiplayerFPS.Scripts
{
    [DefaultExecutionOrder(-100)]
    public class GameRoot : MonoBehaviour
    {
        void Awake()
        {
            Register();
        }

        void Register()
        {
            var inputService = new MouseKeyboardInputService();
            ServiceLocator.Current.Register<IInputService>(inputService);
        }

        void Unregister()
        {
            ServiceLocator.Current.Unregister<IInputService>();
        }

        void OnDestroy()
        {
            Unregister();
        }
    }
}