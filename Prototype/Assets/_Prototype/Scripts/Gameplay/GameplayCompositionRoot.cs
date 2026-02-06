using System;
using _Prototype.Scripts.Gameplay.Services.InputService;
using _Prototype.Scripts.Utils.ServiceLocator;
using UnityEngine;

[DefaultExecutionOrder(-1)]
public class GameplayCompositionRoot : MonoBehaviour
{
    public void Awake()
    {
        var inputService = new KeyboardInputService();
        ServiceLocator.Current.Register(inputService);
    }
}
